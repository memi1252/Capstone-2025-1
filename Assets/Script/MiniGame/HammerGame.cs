using System;
using UnityEngine;

public class HammerGame : MonoBehaviour
{
    [SerializeField] private Animator HammerAnim;
    [SerializeField] private AudioSource HammerAudio;

void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HammerAnim.SetTrigger("HitHammer");
            HammerAnim.SetTrigger("OutHitHammer");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hello");
        HammerAudio.Play();
    }
}
