using System;
using UnityEngine;

public class SpaceDoorOpen : MonoBehaviour
{
    private Animator animator;

    private bool colin;
    public bool isOpen = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            audioSource.Play();
            animator.SetBool("Opened", false);
            animator.SetTrigger("Actived");
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"  && isOpen)
        {
            colin = false;
            audioSource.Play();
            animator.SetBool("Opened", true);
            animator.SetTrigger("Actived");
        }
    }
}
