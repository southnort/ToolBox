using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 rotationSpeed;



    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
