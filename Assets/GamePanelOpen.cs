using UnityEditor;
using UnityEngine;

public class GamePanelOpen : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject Player;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePanel.activeSelf == false)
            {
                MouseCursor(true);
                gamePanel.SetActive(true);
            }
            else
            {
                MouseCursor(false);
                gamePanel.SetActive(false);
            }
        }
    }
    
    public void MouseCursor(bool isShow)
    {
        if (isShow)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
