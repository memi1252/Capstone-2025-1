using UnityEngine;

public class SettigExit : MonoBehaviour
{
    [SerializeField] private GameObject SettingUI;
    
    public void SettingOut()
    {
        SettingUI.SetActive(false);
    }
}
