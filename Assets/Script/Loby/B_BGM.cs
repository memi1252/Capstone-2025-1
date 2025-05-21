using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class B_BGM : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    public void AudioControl()
    {
        float sound = volumeSlider.value;
        
        if(sound <= -40f) audioMixer.SetFloat("Bgound", -80);
        else audioMixer.SetFloat("Bgound", sound);
    }
    
}
    