using System;
using System.Collections;
using UnityEngine;

public class SpaceDoorOpen : MonoBehaviour
{
    private Animator animator;

    private bool colin;
    public bool isOpen = false;
    public bool opned = false;
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

    public void Open()
    {
        if(!isOpen) return;
        colin = true;
        opned = true;
        audioSource.Play();
        animator.SetBool("Opened", false);
        animator.SetTrigger("Actived");
        StartCoroutine(timeClose());
    }

    IEnumerator timeClose()
    {
        yield return new WaitForSeconds(5f);
        Close();
    }

    public void Close()
    {
        colin = false;
        opned = false;
        audioSource.Play();
        animator.SetBool("Opened", true);
        animator.SetTrigger("Actived");
    }

    
}
