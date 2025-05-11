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
        otherUI.SetActive(false);
    }
}
