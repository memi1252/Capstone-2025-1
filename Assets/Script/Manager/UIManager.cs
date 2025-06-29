using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public TooltipUI tooltipUI;
    public GameObject InvneoryUI;
    public GameObject QuitSlotUI;
    public GameObject ProductSlotUI;
    public GameObject ProductUI;
    public GameObject StastUI;
    public GameObject itemDescriptionUI;
    public TutorialsUI tutorialsUI;
    public GameObject BBASSViewUI;
    public dayContViewUI dayContViewUI;
    public GameObject ESCMENUUI;
    public GameObject SettingUI;
    public GameObject combination1;
    public GameObject combination2;
    public GameObject combination3;
    public GameObject GameOverUI;
    
    
    [Header("sound slider")]
    public Slider MasSoundSlider;
    public Slider BGMVolumeSlider;
    public Slider SFXVolumeSlider;
}
