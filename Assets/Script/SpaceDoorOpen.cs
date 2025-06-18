using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceDoorOpen : MonoBehaviour
{
    private Animator animator;

    private bool colin;
    public bool isOpen = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }

    private bool BBASSMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isOpen)
        {
            colin = true;
            animator.SetBool("Opened", false);
            animator.SetTrigger("Actived");
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"  && isOpen)
        {
            colin = false;
            animator.SetBool("Opened", true);
            animator.SetTrigger("Actived");
        }
    }
}
