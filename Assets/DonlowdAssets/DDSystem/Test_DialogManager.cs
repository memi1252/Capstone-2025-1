using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doublsb.Dialog;
using Unity.VisualScripting;

public class Test_DialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject Printer; // 대화창 오브젝트
    public TextMeshProUGUI PrinterText; // 출력될 텍스트

    [Header("Audio")]
    public AudioSource SEAudio; // 타이핑 효과음

    [Header("설정")]
    public float Delay = 0.1f; // 글자 간 딜레이

    private Coroutine printingRoutine;

    // Test_TestMessage_Selection에서 대사 리스트를 받아 출력
    public void Show(List<DialogData> dataList)
    {
        if (printingRoutine != null)
            StopCoroutine(printingRoutine);

        printingRoutine = StartCoroutine(PrintDialogList(dataList));
    }

    // 대사 리스트 순서대로 출력
    private IEnumerator PrintDialogList(List<DialogData> dataList)
{
    Printer.SetActive(true);  // 대화창 표시

    foreach (var data in dataList) //dataList 길이만큼 반복복
    {
           
                foreach (var command in data.Commands)
                {
                    if (command.Command == Command.print)
                    {
                        yield return StartCoroutine(PrintText(command.Context));
                        yield return new WaitForSeconds(0.6f); // 문장 간 텀
                    }
                }
        
    }

    Printer.SetActive(false); // 출력 종료 시 숨김
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
}
