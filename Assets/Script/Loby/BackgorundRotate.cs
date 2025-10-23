using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgorundRotate : MonoBehaviour
{

    [SerializeField] private float Speed;
    void Update()
    {
        Speed = Time.time * 3f;
        RenderSettings.skybox.SetFloat("_Rotation", Speed);
    }

    public Slider Slider1;
    public Slider Slider2;
    public Slider Slider3;
}
