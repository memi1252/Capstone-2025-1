using System;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool isCut = false;
    public bool isCount;
    public bool isMouseOver = false;

    private void Update()
    {
        if (isMouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCut = true;
                Debug.Log("Wire Cut");
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "CutObject")
        {
            isMouseOver = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "CutObject")
        {
            isMouseOver = false;
        }
    }
}
