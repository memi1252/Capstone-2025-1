using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class BBASS_MentBASE : MonoBehaviour
{
    [Header("UI")]
    public GameObject Printer; //대화창 오브젝트 
    public TextMeshProUGUI PrinterText; //출력될 텍스트

    [Header("Audio")]
    public AudioSource SEAudio; //타이핑 효과음

    [Header("설정")]
    public float Delay = 0.1f; //글자 간 딜레이
    public bool LookAtCamera = true; //카메라를 바라볼지 여부

    private Coroutine printingRoutine;
    
    
    public bool isPlay = false; //대사 출력 중인지 여부
    private Quaternion originalRotation; 
    
    //Test_TestMessage_Selection에서 대사 리스트를 받아 출력
    public void Show(List<DialogData> dataList)
    {
        if (printingRoutine != null)
            StopCoroutine(printingRoutine);
        printingRoutine = StartCoroutine(PrintDialogList(dataList));
    }

    

    //대사 리스트 순서대로 출력
    public virtual IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        

        if (LookAtCamera)
        {
            originalRotation = GameManager.Instance.BBASS.transform.rotation;
            Vector3 targetRotation = Camera.main.transform.position - GameManager.Instance.BBASS.transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetRotation);
            while (GameManager.Instance.BBASS.transform.rotation.eulerAngles != rotation.eulerAngles)
            {
                GameManager.Instance.BBASS.transform.rotation = Quaternion.RotateTowards(GameManager.Instance.BBASS.transform.rotation, rotation, 100f* Time.deltaTime);
                yield return null;
            }
        }
        Printer.SetActive(true);  //대화창 표시
        isPlay = true; //대사 출력 중 상태로 변경
        GameManager.Instance.BBASSPlay = true;
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

        if (LookAtCamera)
        {
            while (GameManager.Instance.BBASS.transform.rotation.eulerAngles != originalRotation.eulerAngles)
            {
                GameManager.Instance.BBASS.transform.rotation = Quaternion.RotateTowards(GameManager.Instance.BBASS.transform.rotation, originalRotation, 100f* Time.deltaTime);
                yield return null;
            }
        }
        isPlay = false; //대사 출력 완료 상태로 변경
        GameManager.Instance.BBASSPlay = false;
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
            if (LookAtCamera)
            {
                GameManager.Instance.BBASS.transform.LookAt(Camera.main.transform.position);
            }
            yield return new WaitForSeconds(Delay);
        }
    }
}
