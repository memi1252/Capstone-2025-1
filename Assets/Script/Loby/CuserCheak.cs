using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuserCheak : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    void Update()
    {
        /*if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            text.color = new Color32(144, 254, 255, 255);
        }
        else
        {
            text.color = new Color32(255,255,255,255);
        }*/
        
        
    }

    void OnMouseEnter()
    {
        text.color = new Color32(144, 254, 255, 255);
    }

    private void OnMouseExit()
    {
        text.color = new Color32(255,255,255,255);
    }

    /*private void OnMouseOver()
    {
        Debug.Log("MouseOver");
    }*/
}
