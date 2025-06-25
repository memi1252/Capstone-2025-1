using UnityEditor;
using UnityEngine;

public class GamePanelOpen : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.isItemPickUp)
        {
            if (gamePanel.activeSelf == false)
            {
                MouseCursor(true);
                gamePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                MouseCursor(false);
                gamePanel.SetActive(false);
                Time.timeScale = 1;
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
