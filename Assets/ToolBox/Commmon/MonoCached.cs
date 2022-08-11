using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToolBox
{
    public class MonoCached : MonoBehaviour
    {
        /// <summary>
        /// Custom change for standart MonoBehaviour
        /// Optimize vetdods like a Update()
        /// !!!! Need UpdateManager placed on scene !!!!    
        /// </summary>

        public static List<MonoCached> allTicks = new List<MonoCached>(500);
        public static List<MonoCached> allFixedTicks = new List<MonoCached>(30);
        public static List<MonoCached> allLateTicks = new List<MonoCached>(30);

        protected virtual void OnEnable()
        {
            allTicks.Add(this);
        }

        private void OnDisable()
        {
            allTicks.Remove(this);
            if (allFixedTicks.Contains(this))
                allFixedTicks.Remove(this);
            if (allLateTicks.Contains(this))
                allLateTicks.Remove(this);
        }


        public void Tick()
        {
            OnTick();
        }

        public void FixedTick()
        {
            OnFixedTick();
        }

        public void LateTick()
        {
            OnLateTick();
        }



        public void AddToFixedList()
        {
            if (!allFixedTicks.Contains(this))
                allFixedTicks.Add(this);
        }

        public void AddToLateList()
        {
            if (!allLateTicks.Contains(this))
                allLateTicks.Add(this);
        }



        public virtual void OnTick()
        {
        }
        public virtual void OnFixedTick()
        {
        }
        public virtual void OnLateTick()
        {
        }


        public void DoWithDelay(System.Action method, float delayTime)
        {
            StartCoroutine(DelayCoroutine(method, delayTime));
        }

        private IEnumerator DelayCoroutine(System.Action method, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            method();
        }
    }
}