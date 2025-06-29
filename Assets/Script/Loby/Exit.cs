using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {

#if UNITY_EDITOR
        PlayerPrefs.SetFloat("BackgroundVolume", SoundManager.Instance.BackgroundSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", SoundManager.Instance.MasterSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", SoundManager.Instance.EffectSlider.value);
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 실행 중지
#else
        PlayerPrefs.SetFloat("BackgroundVolume", SoundManager.Instance.BackgroundSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", SoundManager.Instance.MasterSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", SoundManager.Instance.EffectSlider.value);
        Application.Quit();  // 빌드된 게임 종료
#endif
    }
}
