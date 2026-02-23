using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
            dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story12")));
            dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story13")));
            Show(dialogTexts);
        }
        else
        {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story14")));
            Show(dialogTexts);
        }
    }
}
