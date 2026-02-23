using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SPACESTART : BBASS_MentBASE
{
    
    public Animator Animator;
    public float AnimationDelay = 0.5f; //애니메이션 딜레이

    public GameObject camera;

    public GameObject doking;
    public GameObject pos1;
    public bool ispos1;
    public GameObject[] Combinations1;
    public bool BBASSMove = false; //BBASS가 움직이는지 여부
    
    //Test_TestMessage_Selection에서 대사 리스트를 받아 출력
    

    private void Update()
    {
        //스킵
        if (Input.GetKeyDown(KeyCode.H))
        {
            StopAllCoroutines();
            GameManager.Instance.player.gameObject.SetActive(true);
            camera.SetActive(false);
            Printer.SetActive(false);
            GameManager.Instance.ismove = true;
            GameManager.Instance.isCamera = true;
            UIManager.Instance.StastUI.SetActive(true);
            UIManager.Instance.QuitSlotUI.SetActive(true);
            GameManager.Instance.MouseCursor(false);
            ispos1 = true;
            Printer.SetActive(false);
            
            WayPointUI.Instance.isActive = true;    
            UIManager.Instance.tutorialsUI.MoveOn();
            GameManager.Instance.noInventoryOpen = false;
            GameManager.Instance.BBASS.GetComponent<Collider>().enabled = false;
        }

        if (ispos1)
        {
            BBASSMove = true;
            GameObject BBASS = GameObject.FindGameObjectWithTag("BBASS").transform.GetChild(0).gameObject;
            BBASS.GetComponent<Animator>().enabled = false;
            BBASS = BBASS.transform.parent.gameObject;
            if (BBASS != null)
            {
                if(BBASS.transform.position != pos1.transform.position)
                {
                    BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position, pos1.transform.position, Time.deltaTime * 2);
                    BBASS.transform.LookAt(pos1.transform.position);
                }
                else
                {
                    if (BBASS.transform.rotation != pos1.transform.rotation)
                    {
                        BBASS.transform.rotation = Quaternion.RotateTowards(BBASS.transform.rotation, pos1.transform.rotation, Time.deltaTime * 100);
                    }
                    else
                    {
                        ispos1 = false;
                        BBASSMove = false;
                        BBASS.transform.position =pos1.transform.position;
                        GameManager.Instance.BBASS.GetComponent<Collider>().enabled = true;
                        BBASS.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }
    }

    private bool first = false;
    private bool second = true;
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        if (!first)
        {
            Animator.SetTrigger("Show");
            yield return new WaitForSeconds(AnimationDelay);
        }
        
        yield return StartCoroutine(base.PrintDialogList(dataList));

        if (!first)
        {
            GameManager.Instance.player.gameObject.SetActive(true);
            camera.SetActive(false);
            GameManager.Instance.ismove = true;
            GameManager.Instance.isCamera = true;
            UIManager.Instance.StastUI.SetActive(true);
            UIManager.Instance.QuitSlotUI.SetActive(true);
            GameManager.Instance.MouseCursor(false);
            UIManager.Instance.tutorialsUI.MoveOn();
            ispos1 = true;
            first = true;
            second = false;
            GameManager.Instance.BBASS.GetComponent<Collider>().enabled = false;
            GameManager.Instance.noInventoryOpen = false;
        }

        if (second)
        {
            Printer.SetActive(false);
        }

        if (!second)
        {
            Second();
            Combinations1[0].SetActive(true);
            Combinations1[1].SetActive(true);
            GameManager.Instance.sitcar = true;
            second = true;
            WayPointUI.Instance.isActive = true;
        }
        
        
    }
    
    

   
    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story1")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story2")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story3")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story4")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story5")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story6")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story7")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story8")));
        
        Show(dialogTexts);
    }

    private void Second()
    {
        var dialogTexts = new List<DialogData>();
        
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story9")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story10")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story11")));
        
        Show(dialogTexts);
    }
}
