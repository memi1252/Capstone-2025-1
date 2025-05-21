using System;
using UnityEngine;

public class ScrewDriver : MonoBehaviour
{
    private Animator animator;
    private bool isMouseOver = false;
    private bool isDragging = false;
    private bool isuse;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButton(0) && !isuse)
        {
           animator.SetTrigger("use");
           transform.parent.GetComponent<ScrewPoint>().count++;
           isuse = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            isMouseOver = false;
            isuse = false;
        }
    }

    private void OnMouseDown()
    {
        if (isMouseOver)
        {
            isDragging = true;
        }
    }

    private void OnMouseOver()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        
    }
    
    
}
