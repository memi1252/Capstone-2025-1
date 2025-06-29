using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int SceneNumber;
    
    public void ClickButton()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("BackgroundVolume", SoundManager.Instance.BackgroundSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", SoundManager.Instance.MasterSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", SoundManager.Instance.EffectSlider.value);
        SceneManager.LoadScene(SceneNumber);
        //GameManager.Instance.isCamera = true;
    }
}
