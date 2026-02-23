using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;



public class SpaceShip : BBASS_MentBASE
{
    [SerializeField] private GameObject posDoorFront;

    public bool isDoorFront;

    private GameObject BBASS;
    public float Range;
    public Transform pos;
    
    private int currentTargetIndex = 0;

    private void Start()
    {
        BBASS = GameObject.FindGameObjectWithTag("BBASS");
    }

    private void Update()
    {
        
        if (isDoorFront)
        {
            if(BBASS.transform.position!= posDoorFront.transform.position)
            {
                BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position, posDoorFront.transform.position, 3f * Time.deltaTime);
                BBASS.transform.LookAt(posDoorFront.transform.position);
            }
            else
            {
                if (BBASS.transform.rotation != posDoorFront.transform.rotation)
                {
                    BBASS.transform.rotation = Quaternion.RotateTowards(BBASS.transform.rotation, posDoorFront.transform.rotation, 100f * Time.deltaTime);
                }
                else
                {
                    if (DoorFrontMent)
                    {
                        DoorFront();
                        DoorFrontMent = false;
                        isDoorFront = false;
                    }
                }
            }
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range);
        bool isplayer = false;
        foreach (Collider item in colliders)
        {
            if (item.CompareTag("Player"))
            {
                isplayer = true;
                
            }
        }

        if (isplayer)
        {
            if (GetComponent<SpaceShipIn>().inside)
            {
                UIManager.Instance.StastUI.GetComponent<OtherUIvalue>().spaceinSideObj.SetActive(false);
                return;
            }

            if (QuestManager.Instance.quests[QuestManager.Instance.currentQuestIndex]
                    .doors[QuestManager.Instance.doorindex].index > 1)
            {
                UIManager.Instance.StastUI.GetComponent<OtherUIvalue>().spaceinSideObj.SetActive(false);
                return;
            }
            UIManager.Instance.StastUI.GetComponent<OtherUIvalue>().spaceinSideObj.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<SpaceShipIn>().inside = true;
                GameManager.Instance.player.transform.position = pos.position;
                QuestManager.Instance.quests[QuestManager.Instance.currentQuestIndex]
                    .doors[QuestManager.Instance.doorindex].open = true;
                QuestManager.Instance.doorindex++;
                QuestManager.Instance.dd();
            }
        }
        else
        {
            UIManager.Instance.StastUI.GetComponent<OtherUIvalue>().spaceinSideObj.SetActive(false);
        }
        
    }
    
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        yield return StartCoroutine(base.PrintDialogList(dataList));
        
        Printer.SetActive(false);
        foreach (var doorOpen in FindObjectsByType<SpaceDoorOpen>((FindObjectsSortMode)FindObjectsInactive.Include))
        {
            doorOpen.isOpen = true;
        }
        GameManager.Instance.spaceStationEntranceHelpUI.SetActive(true);
    }

    private bool DoorFrontMent = true;
    private void DoorFront()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story32")));
        
        Show(dialogTexts);
    }

    private void NipperMatCheck()
    {
        // 인벤토리에 아이템이 있는지 체크하는 코드
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
