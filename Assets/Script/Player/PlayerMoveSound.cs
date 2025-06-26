using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip walkSound2;
    public AudioClip runSound;
    public AudioClip runSound2;
    public AudioSource playerAudioSource;

    void Update()
    {

    }
    void WalkSound()
    {
        playerAudioSource.PlayOneShot(walkSound);
    }
    void WalkSound2()
    {
        playerAudioSource.PlayOneShot(walkSound2);
    }

    void RunSound()
    {
        playerAudioSource.PlayOneShot(runSound);
    }
    void RunSound2()
    {
        playerAudioSource.PlayOneShot(runSound2);
    }
}
