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
    public GameObject spaceStationEntranceHelpUI;
    
    public bool isSpace = true;
    public bool ismove = true;
    public bool inSpaceShip = true;
    public bool isCamera;
    public bool miniGameScene = true;
    public bool noInventoryOpen = true;
    public bool isInventoryOpen = false;
    public bool noESC = false;
    public bool isdoking = false;
    
    private bool isNipperMat = false;
    public bool[] nipperMax = new bool[3];
    
    private bool ismongkiMat = false;
    public bool[] mongkiMax = new bool[3];
    public int[] mongkiMaxCount;
    public int[] mongkiCount;
    
    private bool isFliterMat = false;
    public bool[] fliterMax = new bool[3];
    public int[] fliterMaxCount;
    public int[] fliterCount;
    
    public bool BBASSPlay = false;
    public bool firstItemmat = false;
    public bool secondItemmat = false;
    public bool thirdItemmat = false;
    public bool CardKetDoor1 = false;
    public bool CardKetDoor2 = false;
    

    private Volume volume;
    public DepthOfField depthOfField;

    private void Start()
    {
        volume = Camera.main.transform.GetComponent<Volume>();
        volume.profile.TryGet(out depthOfField);
        fliterMaxCount[0] = 1;
        fliterMaxCount[1] = 4;
        fliterMaxCount[2] = 4;
    }

    private void Update()
    {
        InventoryOpen();
        NipperMatCheck();
        NmongkiMatCheck();
        CardKet1Check();
        CardKeyDoorOpen();
        MENUOpen();
        Cardkey1Check();
        fliterMatCheck();
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

    private void MENUOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !noESC && !BBASSPlay)
        {
            if (!UIManager.Instance.ESCMENUUI.activeSelf)
            {
                MouseCursor(true);
                UIManager.Instance.ESCMENUUI.SetActive(true);
                depthOfField.active = true;
                noInventoryOpen = true;
                isCamera = false;
                UIManager.Instance.tutorialsUI.move.SetActive(false);
                UIManager.Instance.tutorialsUI.interaction.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                if(!isdoking)
                    MouseCursor(false);
                UIManager.Instance.ESCMENUUI.SetActive(false);
                depthOfField.active = false;
                noInventoryOpen = false;
                isCamera = true;
                UIManager.Instance.SettingUI.SetActive(false);
                if (!UIManager.Instance.tutorialsUI.ismove)
                {
                    UIManager.Instance.tutorialsUI.move.SetActive(true);
                }
                else if (!UIManager.Instance.tutorialsUI.isinteract)
                {
                    UIManager.Instance.tutorialsUI.interaction.SetActive(true);
                }
                Time.timeScale = 1;
            }
        }
    }
    
    private void InventoryOpen()
    {
        if (Input.GetKeyDown(KeyCode.I) && !noInventoryOpen && !BBASSPlay)
        {
            if (UIManager.Instance.InvneoryUI.activeSelf)
            {
                isCamera = true;
                ismove = true;
                MouseCursor(false);
                depthOfField.active = false;
                isInventoryOpen = false;
                noESC = false;
                UIManager.Instance.InvneoryUI.SetActive(false);
            }
            else
            {
                UIManager.Instance.InvneoryUI.SetActive(true);
                depthOfField.active = true;
                ismove = false;
                isCamera = false;
                isInventoryOpen = true;
                noESC = true;
                MouseCursor(true);
                UIManager.Instance.InvneoryUI.transform.localPosition = new Vector3(-180, 1f, 0);
            }
        }
    }

    private bool nipperPlay =false;
    public bool nipperMakePlay = false;
    public bool nipperMake = false;
    private void NipperMatCheck()
    {
        if (!isNipperMat)
        {
            for (int i = 0; i < nipperMax.Length; i++)
            {
                if (!nipperMax[i])
                {
                    isNipperMat = false;
                    return;
                }
                
                if (i == nipperMax.Length - 1)
                {
                    Debug.Log("!!!!!!!");
                    isNipperMat = true;
                    firstItemmat = true;
                    nipperPlay = true;
                    var dialogTexts = new List<DialogData>();
                    dialogTexts.Add(new DialogData("재료를 모두 모았습니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아가 니퍼를 만들어야 합니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아갑시다."));
                    BBASS.Show(dialogTexts);
                    QuestManager.Instance.quests[3].clear = true;
                    return;
                }
            }
        }

        if (nipperPlay && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            nipperPlay = false;
        }

        if (nipperMakePlay && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            nipperMakePlay = false;
        }
    }
    
    private bool mongkiPlay =false;
    public bool mongkiMakePlay = false;
    public bool mongkiMake = false;
    private void NmongkiMatCheck()
    {
        if (!ismongkiMat)
        {
            for (int i = 0; i < mongkiMax.Length; i++)
            {
                if (!mongkiMax[i])
                {
                    ismongkiMat = false;
                    return;
                }
                
                if(mongkiMaxCount[i] != mongkiCount[i])
                {
                    
                    return;
                }
                
                
                if (i == mongkiMax.Length - 1)
                {
                    Debug.Log("??????");
                    ismongkiMat = true;
                    secondItemmat = true;
                    mongkiMake = true;
                    mongkiPlay = true;
                    var dialogTexts = new List<DialogData>();
                    dialogTexts.Add(new DialogData("재료를 모두 모았습니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아가 몽키스페너를 만들어야 합니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아갑시다."));
                    BBASS.Show(dialogTexts);
                    QuestManager.Instance.quests[11].clear = true;
                    return;
                }
            }
        }

        if (mongkiPlay && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            mongkiPlay = false;
        }

        if (mongkiMakePlay && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            mongkiMakePlay = false;
        }
    }
    
    private bool fliterPlay =false;
    public bool fliterMakePlay = false;
    public bool fliterMake = false;
    private void fliterMatCheck()
    {
        if (!isFliterMat)
        {
            for (int i = 0; i < fliterMax.Length; i++)
            {
                if (!fliterMax[i])
                {
                    isFliterMat = false;
                    return;
                }
                
                if(fliterMaxCount[i] != fliterCount[i])
                {
                    
                    return;
                }
                
                
                if (i == fliterMax.Length - 1)
                {
                    isFliterMat = true;
                    thirdItemmat = true;
                    fliterMake = true;
                    fliterPlay = true;
                    var dialogTexts = new List<DialogData>();
                    dialogTexts.Add(new DialogData("재료를 모두 모았습니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아가 필터를 만들어야 합니다."));
                    dialogTexts.Add(new DialogData("우주선으로 돌아갑시다."));
                    BBASS.Show(dialogTexts);
                    QuestManager.Instance.quests[19].clear = true;
                    return;
                }
            }
        }

        if (fliterPlay && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            fliterPlay = false;
        }

        if (fliterMakePlay && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            fliterMakePlay = false;
        }
    }

    public bool isCardKet1 = false;
    private bool CardKet1 = false;
    private void CardKet1Check()
    {
        if (isCardKet1)
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("카드키를 찾았습니다."));
            dialogTexts.Add(new DialogData("잠긴 문을 열어 봅시다."));
            BBASS.Show(dialogTexts);
            QuestManager.Instance.quests[9].clear = true;
            isCardKet1 = false;
            CardKet1 = true;
        }
        
        if(CardKet1 && !BBASS.isPlay) 
        {
            BBASS.Printer.SetActive(false);
            CardKet1 = false;
        }
    }

    public bool isCardKey2 = false;
    private bool CardKey2;
    private void Cardkey1Check()
    {
        if (isCardKey2)
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("카드키를 찾았습니다."));
            dialogTexts.Add(new DialogData("해당키는 2개의 문을 열수있습니다."));
            dialogTexts.Add(new DialogData("잠긴 문들을 열어 봅시다."));
            BBASS.Show(dialogTexts);
            QuestManager.Instance.quests[10].clear = true;
            isCardKey2 = false;
            CardKey2 = true;
        }
        
        if(CardKey2 && !BBASS.isPlay) 
        {
            BBASS.Printer.SetActive(false);
            CardKey2 = false;
        }
    }

    public bool CardKetDoorOpen1 = false;
    public bool CardKeyDoorOpen2 = false;
    private void CardKeyDoorOpen()
    {
        if (CardKetDoorOpen1 && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            CardKetDoorOpen1 = false;
        }
        
        if (CardKeyDoorOpen2 && !BBASS.isPlay)
        {
            BBASS.Printer.SetActive(false);
            CardKeyDoorOpen2 = false;
        }
    }
    

}
