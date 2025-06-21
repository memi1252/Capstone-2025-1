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
    public GameObject Combinations1;
    
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

    private bool first;
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
        }

        if (second)
        {
            Printer.SetActive(false);
        }

        if (!second)
        {
            Second();
            Combinations1.SetActive(true);
            second = true;
        }
        
        
    }
    
    

   
    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("드디어 일어나셨군요!!"));
        dialogTexts.Add(new DialogData("안녕하세요, 저는 AI도우미 BBASS입니다"));
        dialogTexts.Add(new DialogData("현재 상황은 우주선이 오작동을 일으켜 GBSW-KSHN으로 못가는 중입니다."));
        dialogTexts.Add(new DialogData("오작동이 일어난 곳은 전력 분배기, 생명 유지장치, 추진 제어판이있습니다"));
        dialogTexts.Add(new DialogData("현재 그 세 곳의 시스템을 고칠수 있는 도구가 없습니다."));
        dialogTexts.Add(new DialogData("다행이도 근처에 우주정거장이있습니다."));
        dialogTexts.Add(new DialogData("우주정거장에 가면 도구를 만들 재료를 구할수 있을것같습니다."));
        dialogTexts.Add(new DialogData("현재 상황을 보면 우주정거장까지 갈수있을것같습니다."));
        dialogTexts.Add(new DialogData("우주정거장으로 이동해 주세요"));
        
        Show(dialogTexts);
    }

    private void Second()
    {
        var dialogTexts = new List<DialogData>();
        
        dialogTexts.Add(new DialogData("현재 필요한 도구의 조합법을 제작대에 띄어 놓았습니다"));
        
        Show(dialogTexts);
    }
}
