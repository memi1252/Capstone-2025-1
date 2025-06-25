using System;
using System.Collections.Generic;
using Doublsb.Dialog;
using InventorySystem;
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
    [SerializeField] public BBASS_MentBASE BBASS;
    public string WireConnectionScene;
    public string ReplacingPartsScene;
    public Material outlineMaterial;
    public GameObject DirectionalLight;

    public ProductionSystem ProductionSystem1;
    public ProductionSystem ProductionSystem2;
    
    public bool isSpace = true;
    public bool ismove = true;
    public bool inSpaceShip = true;
    public bool isCamera;
    public bool isItemPickUp = false;
    public bool miniGameScene = true;
    public bool noInventoryOpen = true;
    public bool isInventoryOpen = false;
    
    private bool isNipperMat = false;
    public bool[] nipperMax = new bool[3] {false, false, false};
    
    

    private Volume volume;
    private DepthOfField depthOfField;

    private void Start()
    {
        volume = Camera.main.transform.GetComponent<Volume>();
        volume.profile.TryGet(out depthOfField);
    }

    private void Update()
    {
        InventoryOpen();
        NipperMatCheck();
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
                isInventoryOpen = false;
                UIManager.Instance.InvneoryUI.SetActive(false);
            }
            else
            {
                UIManager.Instance.InvneoryUI.SetActive(true);
                depthOfField.active = true;
                ismove = false;
                isCamera = false;
                isInventoryOpen = true;
                MouseCursor(true);
                UIManager.Instance.InvneoryUI.transform.localPosition = new Vector3(-180, 1f, 0);
            }
        }
    }

    private void NipperMatCheck()
    {
        if (!isNipperMat)
        {
            foreach (var mat in nipperMax)
            {
                if(mat== false)
                {
                    isNipperMat = false;
                    return;
                }
                if(mat == nipperMax[nipperMax.Length - 1])
                {
                    QuestManager.Instance.quests[3].clear = true;
                    var dialogTexts = new List<DialogData>();
                    dialogTexts.Add(new DialogData("재료를 모두 모았습니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아가 니퍼를 만들어애 합니다."));
                    dialogTexts.Add(new DialogData("오늘의 우주선으로 돌아갑시다."));
                    BBASS.Show(dialogTexts);
                    isNipperMat = true;
                    return;
                }
            }
        }
    }

    
}
