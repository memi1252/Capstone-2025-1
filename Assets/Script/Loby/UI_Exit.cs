using UnityEngine;

public class UI_Exit : MonoBehaviour
{
    [SerializeField] private GameObject SettingUI;
    
    public void SettingOut()
    {
        SettingUI.SetActive(false);
    }
}
