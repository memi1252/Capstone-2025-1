using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image languageImage;

    void Awake()
    {
        gameObject.SetActive(false);
        if(PlayerPrefs.HasKey("Language"))
        {
            string language = PlayerPrefs.GetString("Language");
            if (language == "ko")
            {
                languageImage.sprite = sprites[0];
            }
            else
            {
                languageImage.sprite = sprites[1];
            }
        }
    }

    public void SetLanguageToKorean()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.CurrentLanguage = "ko";
            LocalizationManager.Instance.RefreshAllText();
            PlayerPrefs.SetString("Language", "ko");
            languageImage.sprite = sprites[0];
        }
    }

    public void SetLanguageToEnglish()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.CurrentLanguage = "en";
            LocalizationManager.Instance.RefreshAllText();
            PlayerPrefs.SetString("Language", "en");
            languageImage.sprite = sprites[1];
        }
    }
}