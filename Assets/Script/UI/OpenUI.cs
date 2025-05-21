using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] private GameObject Open_UI;

    public void ClickButton()
    {
        if (Open_UI.activeSelf == false)
        {
            Open_UI.SetActive(true); 
        }
        else Open_UI.SetActive(false);
    }
}