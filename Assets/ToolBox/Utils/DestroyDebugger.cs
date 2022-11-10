using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox
{
    public class DestroyDebugger : MonoBehaviour
    {
        private void OnDestroy()
        {
            Debug.Log("Im destroyed: " + gameObject.name);
        }
    }
}
