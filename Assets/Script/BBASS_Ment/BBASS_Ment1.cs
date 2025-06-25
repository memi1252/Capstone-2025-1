using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class BBASS_Ment1 : BBASS_MentBASE
{
   

    private bool first = false;

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
        
        if(!first) first = true;
        play = false;
        Printer.SetActive(false);
    }
    

    public void line()
    {
        if (!first)
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("우주정거장으로 이동합시다!!"));
            dialogTexts.Add(new DialogData("조종실로 이동해주세요"));
            Show(dialogTexts);
        }
        else
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("AI도우미 BBASS입니다."));
            Show(dialogTexts);
        }
    }
}
