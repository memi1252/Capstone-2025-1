using UnityEngine;

public class SettingUI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [SerializeField] private GameObject settingUI;    
    
    public void SettingButton()
    {
        if (settingUI.activeSelf == false)
        {
            settingUI.SetActive(true);
        }
        else settingUI.SetActive(false);
    }
}
