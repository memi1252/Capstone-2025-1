using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class BBASS_Ment2 : BBASS_MentBASE
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
        yield return StartCoroutine(base.PrintDialogList(dataList));

        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        
        
        play = false;
        Printer.SetActive(false);
    }
    

    public void line()
    {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("무사히 우주정거장에 도착했습니다."));
            dialogTexts.Add(new DialogData("일단 우주정거장으로 가봅시다."));
            Show(dialogTexts);
        
    }
}
