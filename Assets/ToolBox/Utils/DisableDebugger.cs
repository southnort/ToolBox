using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToolBox
{
    public class DisableDebugger : MonoBehaviour
    {
        private void OnDisable()
        {
            Debug.Log("Im disabled: " + gameObject.name);
        }
    }

}