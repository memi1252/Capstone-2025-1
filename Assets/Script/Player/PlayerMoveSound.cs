using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioSource playerAudioSource;

    void Update()
    {

    }
    void WalkSound()
    {
        playerAudioSource.PlayOneShot(walkSound);
    }
    void RunSound()
    {
        playerAudioSource.PlayOneShot(runSound);
    }
    void JumpSound()
    {
        playerAudioSource.PlayOneShot(jumpSound);
    }
}
