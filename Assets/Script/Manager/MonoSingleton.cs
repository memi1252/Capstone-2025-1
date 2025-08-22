using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if(Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}