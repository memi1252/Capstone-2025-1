using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider BackgroundSlider;
    [SerializeField] private Slider EffectSlider;

    public void Update()
    {
        audioMixer.SetFloat("Background", Mathf.Log10(BackgroundSlider.value) * 20);
        audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
        audioMixer.SetFloat("Effect", Mathf.Log10(EffectSlider.value) * 20);

    }
}
