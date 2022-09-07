using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToolBox
{
    public class EnableDebugger : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log("Im enabled: " + gameObject.name);
        }
    }
}
