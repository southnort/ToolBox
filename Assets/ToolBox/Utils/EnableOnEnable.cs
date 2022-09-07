using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToolBox
{
    public class EnableOnEnable : MonoBehaviour
    {
        [SerializeField] GameObject[] enableOnEnable;

        private void OnEnable()
        {
            foreach (var go in enableOnEnable)
            {
                go.SetActive(true);
            }
        }

        private void OnDisable()
        {
            foreach (var go in enableOnEnable)
            {
                go.SetActive(false);
            }
        }

    }
}