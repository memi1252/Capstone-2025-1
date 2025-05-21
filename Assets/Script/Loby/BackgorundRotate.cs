using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgorundRotate : MonoBehaviour
{

    [SerializeField] private float Speed;
    void Update()
    {
        Speed = Time.time * 3f;
        RenderSettings.skybox.SetFloat("_Rotation", Speed);
    }
}
