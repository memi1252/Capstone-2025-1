using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
        QuestManager.Instance.quests[1].clear = true;
        FindAnyObjectByType<SpaceShip>().isDoorFront = true;
        enabled = false;
    }
    

    public void line()
    {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story15")));
            dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story16")));
            Show(dialogTexts);
        
    }
}
