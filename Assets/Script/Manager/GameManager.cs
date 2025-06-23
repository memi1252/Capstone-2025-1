using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player")]   
    [SerializeField] public Player player;
    [SerializeField] public PlayerCamera playerCamera;
    public string WireConnectionScene;
    public string ReplacingPartsScene;
    public Material outlineMaterial;
    public GameObject DirectionalLight;

    public bool isSpace = true;
    public bool ismove = true;
    public bool inSpaceShip = true;
    public bool isCamera;
    public bool isItemPickUp = false;
    public bool miniGameScene = true;
    public bool noInventoryOpen = true;

    private Volume volume;
    private DepthOfField depthOfField;

    private void Start()
    {
        volume = Camera.main.transform.GetComponent<Volume>();
        volume.profile.TryGet(out depthOfField);
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.J))  //미니게임 전선연결 오픈
        {
            if (miniGameScene)
            {
                SceneManager.LoadScene(WireConnectionScene, LoadSceneMode.Additive);
                ismove = false;
                isCamera = false;
                MouseCursor(true);
                UIManager.Instance.StastUI.SetActive(false);
                UIManager.Instance.QuitSlotUI.SetActive(false);
                playerCamera.gameObject.SetActive(false);
                miniGameScene = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.L))  //미니게임 부품교체 오픈
        {
            if (miniGameScene)
            {
                SceneManager.LoadScene(ReplacingPartsScene, LoadSceneMode.Additive);
                ismove = false;
                isCamera = false;
                MouseCursor(true);
                UIManager.Instance.StastUI.SetActive(false);
                UIManager.Instance.QuitSlotUI.SetActive(false);
                playerCamera.gameObject.SetActive(false);
                miniGameScene = false;
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
        if (Input.GetKeyDown(KeyCode.I) && !isItemPickUp && !noInventoryOpen)
        {
            if (UIManager.Instance.InvneoryUI.activeSelf)
            {
                isCamera = true;
                ismove = true;
                MouseCursor(false);
                depthOfField.active = false;
                UIManager.Instance.InvneoryUI.SetActive(false);
            }
            else
            {
                UIManager.Instance.InvneoryUI.SetActive(true);
                depthOfField.active = true;
                ismove = false;
                isCamera = false;
                MouseCursor(true);
                UIManager.Instance.InvneoryUI.transform.localPosition = new Vector3(-180, 1f, 0);
            }
        }
    }
}
