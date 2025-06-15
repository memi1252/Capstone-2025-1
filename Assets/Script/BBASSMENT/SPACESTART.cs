using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class SPACESTART : MonoBehaviour
{
    [Header("UI")]
    public GameObject Printer; //대화창 오브젝트 
    public TextMeshProUGUI PrinterText; //출력될 텍스트

    [Header("Audio")]
    public AudioSource SEAudio; //타이핑 효과음

    [Header("설정")]
    public float Delay = 0.1f; //글자 간 딜레이

    private Coroutine printingRoutine;
    public Animator Animator;
    public float AnimationDelay = 0.5f; //애니메이션 딜레이

    public GameObject camera;

    public GameObject doking;

    private bool first =false;
    private bool second =false;
    //Test_TestMessage_Selection에서 대사 리스트를 받아 출력
    public void Show(List<DialogData> dataList)
    {
        if (printingRoutine != null)
            StopCoroutine(printingRoutine);
        printingRoutine = StartCoroutine(PrintDialogList(dataList));
    }

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
        }
        
    }


    //대사 리스트 순서대로 출력
    private IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        if (first == false)
        {
            Animator.SetTrigger("Show");
            yield return new WaitForSeconds(AnimationDelay);
        }
        Printer.SetActive(true);  //대화창 표시

        foreach (var data in dataList) //dataList 길이만큼 반복
        {
            foreach (var command in data.Commands)
            {
                if (command.Command == Command.print)
                {
                    yield return StartCoroutine(PrintText(command.Context));
                    yield return WaitForMouseClick(); //마우스 클릭 대기
                }
            }
        }

        if (!first)
        {
            GameManager.Instance.player.gameObject.SetActive(true);
            camera.SetActive(false);
            Printer.SetActive(false);
            GameManager.Instance.ismove = false;
            GameManager.Instance.isCamera = false;
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("WASD로 이동할수 있고 Shift로 달릴수있어요"));
            dialogTexts.Add(new DialogData("ctrl를 누르면 하강하고 Space를 누르면 상승할수있어요"));
            dialogTexts.Add(new DialogData("F키를 눌러 아이템을 줍거나 상호작용할수있어요"));
            first = true;
            Show(dialogTexts);
        }else if (!second)
        {
            GameManager.Instance.MouseCursor(false);
            GameManager.Instance.ismove = true;
            GameManager.Instance.isCamera = true;
            UIManager.Instance.StastUI.SetActive(true);
            UIManager.Instance.QuitSlotUI.SetActive(true);
            Printer.SetActive(false);
            second = true;
        }
        
        
    }

    private IEnumerator WaitForMouseClick()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        // 클릭했으면 0.1초 정도 대기 (더블클릭 방지)
        yield return new WaitForSeconds(0.1f);
    }




    // 한 문장을 한 글자씩 출력
    private IEnumerator PrintText(string text)
    {
        PrinterText.text = "";
        string current = "";

        for (int i = 0; i < text.Length; i++)
        {
            current += text[i];
            PrinterText.text = current;

            if (text[i] != ' ' && SEAudio != null)
                SEAudio.Play();

            yield return new WaitForSeconds(Delay);
        }
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
