using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class BBASS_Ment3 : BBASS_MentBASE
{
    public bool play = false;

    private void Update()
    {
        if (play)
        {
            UIManager.Instance.tooltipUI.Hide();
        }
    }

    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        play = true;
        UIManager.Instance.combination1.SetActive(false);
        UIManager.Instance.combination2.SetActive(true);
        yield return StartCoroutine(base.PrintDialogList(dataList));
        
        play = false;
        Printer.SetActive(false);
        QuestManager.Instance.quests[7].clear = true;
        foreach (var doorOpen in FindObjectsByType<SpaceDoorOpen>((FindObjectsSortMode)FindObjectsInactive.Include))
        {
            doorOpen.isOpen = true;
        }
        //FindAnyObjectByType<SpaceDoorOpen>().isOpen = true;
        enabled = false;
    }
    

    public void line()
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("편히 주무셨나요?"));
        dialogTexts.Add(new DialogData("오늘은 추진 제어판을 수리해야합니다"));
        dialogTexts.Add(new DialogData("현재 필요한 도구의 조합법은 제작대에 띄워 놓았습니다"));
        dialogTexts.Add(new DialogData("확인 후 우주정거장으로 가서 필요한 재료들를 모아주세요"));
        Show(dialogTexts);
    }
}
