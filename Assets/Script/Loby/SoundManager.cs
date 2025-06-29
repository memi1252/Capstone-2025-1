using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Object = System.Object;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public Slider MasterSlider;
    [SerializeField] public Slider BackgroundSlider;
    [SerializeField] public Slider EffectSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        else
        {
            MasterSlider.value = 1f;
        }
        if (PlayerPrefs.HasKey("BackgroundVolume"))
        {
            BackgroundSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        }
        else
        {
            BackgroundSlider.value = 1f; 
        }
        if (PlayerPrefs.HasKey("EffectVolume"))
        {
            EffectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        }
        else
        {
            EffectSlider.value = 1f;
        }
    }

    public void Update()
    {
        if (MasterSlider == null)
        {
            MasterSlider = GameObject.Find("MasterBGM").GetComponent<Slider>();
            MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (BackgroundSlider == null)
        {
            BackgroundSlider = GameObject.Find("BackgroundBGM").GetComponent<Slider>();
            BackgroundSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        }
        if (EffectSlider == null)
        {
            EffectSlider = GameObject.Find("EffectBGM").GetComponent<Slider>();
            EffectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        }
        audioMixer.SetFloat("Background", Mathf.Log10(BackgroundSlider.value) * 20);
        audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
        audioMixer.SetFloat("Effect", Mathf.Log10(EffectSlider.value) * 20);

    }
}
