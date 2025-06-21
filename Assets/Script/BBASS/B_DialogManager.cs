using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using System.Collections;

public class B_DialogManager : MonoBehaviour
{
    public GameObject Printer;
    public TextMeshProUGUI PrinterText;
    public AudioSource SEAudio;
    public float Delay = 0.1f;

    private DialogData currentData;
    private float currentDelay;
    private float lastDelay;
    private Coroutine textingRoutine;
    private Coroutine printingRoutine;


    // 외부에서 대사 리스트를 받아 출력 시작
    public void Show(List<DialogData> dataList)
    {
        if (printingRoutine != null)
            StopCoroutine(printingRoutine);    // 이전 대사 출력 중이면 멈춤

        printingRoutine = StartCoroutine(PrintDialogList(dataList)); // 새로운 출력 시작
    }

    // 여러 개의 대사를 순차적으로 출력
    private IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        Printer.SetActive(true);               // 대화창 열기
        PrinterText.text = "";                 // 이전 텍스트 초기화

        foreach (DialogData data in dataList)
        {
            yield return StartCoroutine(PrintText(data.PrintText)); // 한 문장 출력
            yield return new WaitForSeconds(0.5f);    // 문장 간 짧은 텀
        }

        Printer.SetActive(false);              // 전부 출력 끝나면 대화창 닫기
    }

    // 한 문장을 한 글자씩 출력하는 코루틴
    private IEnumerator PrintText(string text)
    {
        PrinterText.text = "";                 // 출력 전 텍스트 초기화
        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            result += text[i];                 // 글자 하나 추가
            PrinterText.text = result;         // UI에 갱신

            if (text[i] != ' ' && SEAudio != null)
                SEAudio.Play();                // 공백이 아니면 효과음 재생

            yield return new WaitForSeconds(currentDelay); // 딜레이 적용
        }
    }
}
        
