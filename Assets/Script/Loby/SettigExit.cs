using UnityEngine;

public class SettigExit : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [SerializeField] private GameObject SettingUI;
    
    public void SettingOut()
    {
        SettingUI.SetActive(false);
    }
}
