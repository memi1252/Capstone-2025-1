using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class SPACESTART : BBASS_MentBASE
{
    
    public Animator Animator;
    public float AnimationDelay = 0.5f; //애니메이션 딜레이

    public GameObject camera;

    public GameObject doking;
    public GameObject pos1;
    public bool ispos1;
    
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
            UIManager.Instance.tutorialsUI.MoveOn();
        }

        if (ispos1)
        {
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
                        BBASS.transform.position =pos1.transform.position;
                        BBASS.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }
    }

    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        
        Animator.SetTrigger("Show");
        yield return new WaitForSeconds(AnimationDelay);
        yield return StartCoroutine(base.PrintDialogList(dataList));
    
        
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
        UIManager.Instance.tutorialsUI.MoveOn();
    }

   
    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("드디어 일어나셨군요!!"));
        dialogTexts.Add(new DialogData("안녕하세요, 저는 AI도우미 BBASS입니다"));
        dialogTexts.Add(new DialogData("현재 상황은 우주선이 오작동을 일으켜 행성으로 못가는 중입니다."));
        dialogTexts.Add(new DialogData("다행이도 근처에 우주정거장이있습니다."));
        dialogTexts.Add(new DialogData("우주정거장까지는 이동할수있습니다."));
        dialogTexts.Add(new DialogData("우주정거장에 가면 우주선을 수리할수 있습니다."));
        dialogTexts.Add(new DialogData("우주정거장으로 이동해 주세요"));
        
        Show(dialogTexts);
    }
}
