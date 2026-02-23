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
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story17")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story18")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story19")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story20")));
        Show(dialogTexts);
    }
}
