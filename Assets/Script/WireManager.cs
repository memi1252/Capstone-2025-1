using TMPro;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public static WireManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public TextMeshProUGUI statusText;
    
    public void Success()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "성공";
        statusText.color = Color.green;
    }
    
    public void Fail()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "실패";
        statusText.color = Color.red;
    }
}
