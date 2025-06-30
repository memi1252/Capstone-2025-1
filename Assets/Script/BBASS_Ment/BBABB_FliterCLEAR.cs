using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class BBABB_FliterCLEAR : BBASS_MentBASE
{
    public BBASS_Ment5 BBASS_Ment5;
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        yield return StartCoroutine(base.PrintDialogList(dataList));

        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        BBASS_Ment5.enabled = true;
        Printer.SetActive(false);
    }

    public void Clear()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("성공적으로 수리되었습니다."));
        dialogTexts.Add(new DialogData("이제 행성 HKSN으로 이동 할 수 있습니다"));
        dialogTexts.Add(new DialogData("준비가 다 되셨다면 저에게 알려주세요"));
        QuestManager.Instance.quests[22].clear = true;
        Show(dialogTexts);
        
    }
}
