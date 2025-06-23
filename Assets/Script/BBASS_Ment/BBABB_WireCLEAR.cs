using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class BBABB_WireCLEAR : BBASS_MentBASE
{
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        
        yield return StartCoroutine(base.PrintDialogList(dataList));

        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        
        Printer.SetActive(false);
    }

    public void Clear()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("전선 연결이 완료되었습니다!"));
        dialogTexts.Add(new DialogData("다움 수리에 필요한 도구의 조합법을 제작대에 띄워 놓았습니다."));
        dialogTexts.Add(new DialogData("오늘의 우주선에돌아가서 쉴까요?"));
        Show(dialogTexts);
        
    }
}
