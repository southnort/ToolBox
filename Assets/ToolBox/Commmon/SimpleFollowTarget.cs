using System;
using UnityEngine;


namespace ToolBox
{
    public class SimpleFollowTarget : MonoBehaviour
    {
        [SerializeField] Transform target;
        private Vector3 offset = Vector3.zero;


        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
