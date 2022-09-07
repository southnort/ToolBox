using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToolBox
{
    public class DisableOnEnable : MonoBehaviour
    {
        [SerializeField] GameObject[] disableOnEnable;

        private void OnEnable()
        {
            foreach (var go in disableOnEnable)
            {
                go.SetActive(false);
            }
        }

        private void OnDisable()
        {
            foreach (var go in disableOnEnable)
            {
                go.SetActive(true);
            }
        }

    }
}