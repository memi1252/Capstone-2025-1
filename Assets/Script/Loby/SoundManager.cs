using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public Slider MasterSlider;
    [SerializeField] public Slider BackgroundSlider;
    [SerializeField] public Slider EffectSlider;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (MasterSlider == null)
            {
                MasterSlider = UIManager.Instance.MasSoundSlider;
                MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            }
            if (BackgroundSlider == null)
            {
                BackgroundSlider = UIManager.Instance.BGMVolumeSlider;
                BackgroundSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
            }
            if (EffectSlider == null)
            {
                EffectSlider = UIManager.Instance.SFXVolumeSlider;
                EffectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
            }   
        }
       
        audioMixer.SetFloat("Background", Mathf.Log10(BackgroundSlider.value) * 20);
        audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
        audioMixer.SetFloat("Effect", Mathf.Log10(EffectSlider.value) * 20);

    }
}
