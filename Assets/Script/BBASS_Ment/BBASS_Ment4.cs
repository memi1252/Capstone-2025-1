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
        dialogTexts.Add(new DialogData("비상입니다!"));
        dialogTexts.Add(new DialogData("현재 생명 유지장치가 고장났습니다"));
        dialogTexts.Add(new DialogData("원래 오늘 출발할 예정이였지만 생명 유지장치가 고장나서 출발이 불가능합니다"));
        dialogTexts.Add(new DialogData("생명유지 장치를 수리하기 위해서는 유지 장치의 전원을 끈다음"));
       
        Show(dialogTexts);
    }
}
