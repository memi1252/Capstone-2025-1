using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class BBASS_Ment4 : BBASS_MentBASE
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
        UIManager.Instance.combination2.SetActive(false);
        UIManager.Instance.combination3.SetActive(true);
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
        dialogTexts.Add(new DialogData("오늘도 잘 주무셨나요?"));
        dialogTexts.Add(new DialogData("오늘은 생명 유지장치를 수리해야합니다"));
        dialogTexts.Add(new DialogData("생명 유지장치의 수리 방법은 이러합니다"));
        dialogTexts.Add(new DialogData("장치의 전원을 끈 후 필터를 교체하고 다시 전원을 키면 완료입니다"));
        dialogTexts.Add(new DialogData("필터의 재료는 제작대 화면에 띄어 놓았습니다"));
        Show(dialogTexts);
    }
}
