using System;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    public bool isOpen;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Open()
    {
        animator.SetTrigger("Open");   
        isOpen = true;
    }

    public void Close()
    {
        animator.SetTrigger("Close");
            isOpen = false;
    }
}
