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
        }
        
        
    }
    
    

   
    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("깨어나셨군요 무사하셔서 다행입니다"));
        dialogTexts.Add(new DialogData("안녕하세요, 저는 AI도우미 로봇 BBASS입니다"));
        dialogTexts.Add(new DialogData("현재 상황을 알려드리겠습니다"));
        dialogTexts.Add(new DialogData("행성 HKSN로 가던 중 시스템 오작동으로 인해 더 이상 나아갈 수 없는 상황입니다"));
        dialogTexts.Add(new DialogData("오작동이 일어난 곳은 전력 분배기, 추진 제어판 입니다"));
        dialogTexts.Add(new DialogData("수리를 위해 도구가 필요합니다"));
        dialogTexts.Add(new DialogData("도구는 우주정거장으로 이동해 재료를 구한 다음 직접 만들어야 합니다"));
        dialogTexts.Add(new DialogData("우주정거장으로 이동해 주세요"));
        
        Show(dialogTexts);
    }

    private void Second()
    {
        var dialogTexts = new List<DialogData>();
        
        dialogTexts.Add(new DialogData("왼쪽 위에 체력바는 피로도나 산소가 다 닳게되면 채워질때까지 줄어들게 됩니다"));
        dialogTexts.Add(new DialogData("무엇을 해야할지 모르겠다면 오른쪽 위에 퀘스트 창을 봐주세요"));
        dialogTexts.Add(new DialogData("현재 필요한 도구의 조합법은 제작대에 띄워 놓았습니다"));
        
        Show(dialogTexts);
    }
}
