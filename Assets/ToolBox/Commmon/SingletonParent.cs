using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonParent : MonoBehaviour
{
    protected SingletonParent _Awake(SingletonParent instance)
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("Singleton already created: " + instance.GetType().Name);
            //DestroyImmediate(gameObject);
        }
        return instance;
    }
}