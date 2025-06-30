using System;
using Unity.VisualScripting;
using UnityEngine;

public class OtherUI : MonoBehaviour
{
    [SerializeField] private GameObject otherUI;    
    
    public void ClickButton()
    {
        if (otherUI.activeSelf == false)
        {
            otherUI.SetActive(true);
        }
        else otherUI.SetActive(false);
    }

    public void ClickButton2()
    {
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.ESCMENUUI.SetActive(false);
        GameManager.Instance.depthOfField.active = false;
        GameManager.Instance.noInventoryOpen = false;
        GameManager.Instance.isCamera = true;
        UIManager.Instance.SettingUI.SetActive(false);
        if (!UIManager.Instance.tutorialsUI.ismove)
        {
            UIManager.Instance.tutorialsUI.move.SetActive(true);
        }
        else if (!UIManager.Instance.tutorialsUI.isinteract)
        {
            UIManager.Instance.tutorialsUI.interaction.SetActive(true);
        }
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (otherUI.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                otherUI.SetActive(false);
            }
        }
    }
}
