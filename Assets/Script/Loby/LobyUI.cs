using UnityEngine;

public class LobyUI : MonoBehaviour
{
    [SerializeField] private GameObject lobyUI;    
    
    public void ClickButton()
    {
        if (lobyUI.activeSelf == false)
        {
            lobyUI.SetActive(true);
        }
        else lobyUI.SetActive(false);
    }
}
