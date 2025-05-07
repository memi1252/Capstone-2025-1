using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player")]   
    [SerializeField] public Player player;
    [SerializeField] public PlayerCamera playerCamera;

    public bool isSpace = true;
    public bool ismove = true;
    public bool isCamera;

    private void Start()
    {
        MouseCursor(false);
    }

    private void Update()
    {
        //임시
        if (Input.GetKeyDown(KeyCode.G))
        {
            //미니게임 전선연결 오픈
            UIManager.Instance.wireManager.Show();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            UIManager.Instance.RotaionGame.Show();
        }

        InventoryOpen();
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
    
    private void InventoryOpen()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UIManager.Instance.InvneoryUI.activeSelf)
            {
                isCamera = true;
                ismove = true;
                MouseCursor(false);
                UIManager.Instance.InvneoryUI.SetActive(false);
            }
            else
            {
                UIManager.Instance.InvneoryUI.SetActive(true);
                ismove = false;
                isCamera = false;
                MouseCursor(true);
            }
        }
    }
}
