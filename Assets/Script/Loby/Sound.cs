using UnityEngine;

public class Sound : MonoSingleton<Sound>
{
    public AudioSource buttonClick;
    public AudioSource buttonHover;
    void Start()
    {

    }

    void Update()
    {

    }

    public void soundPlaybutton()
    {
        buttonClick.Play();
    }

    public void SoundPlayHover()
    {
        buttonHover.Play();
    }
}
