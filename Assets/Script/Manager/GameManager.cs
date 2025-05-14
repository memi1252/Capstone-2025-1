using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player")]   
    [SerializeField] public Player player;
    [SerializeField] public PlayerCamera playerCamera;
    public string nextSceneName;

    public bool isSpace = true;
    public bool ismove = true;
    public bool isCamera;
    public bool isItemPickUp = false;
    private bool ss = true;

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (ss)
            {
                SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
                ss = false;
            }
            else
            {
                SceneManager.UnloadSceneAsync(nextSceneName);
                ss = true;
            }
            
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
        if (Input.GetKeyDown(KeyCode.I) && !isItemPickUp)
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
