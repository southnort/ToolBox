using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox
{
    public class UpdateManager : MonoBehaviour
    {
        void Update()
        {
            for (int i = 0; i < MonoCached.allTicks.Count; i++)
            {
                MonoCached.allTicks[i].Tick();
            }
        }

        void FixedUpdate()
        {
            for (int i = 0; i < MonoCached.allFixedTicks.Count; i++)
            {
                MonoCached.allFixedTicks[i].FixedTick();
            }
        }

        void LateUpdate()
        {
            for (int i = 0; i < MonoCached.allLateTicks.Count; i++)
            {
                MonoCached.allLateTicks[i].LateTick();
            }
        }
    }
}