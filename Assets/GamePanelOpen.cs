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
                gamePanel.SetActive(true);
                
            }
            else
            {
                gamePanel.SetActive(false);
            }
        }

        if (gamePanel.activeSelf == false)
        {
            Player.GetComponent<Player>().rotationSpeed = 2f;
            MouseCursor(false);
        }
        else if (gamePanel.activeSelf)
        {
            Player.GetComponent<Player>().rotationSpeed = 0f;
            MouseCursor(true);
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
