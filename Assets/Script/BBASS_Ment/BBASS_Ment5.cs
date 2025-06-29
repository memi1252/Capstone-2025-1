using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBASS_Ment5 : BBASS_MentBASE
{
    public bool play = false;
    public int LoadSceneNumber;

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
        QuestManager.Instance.quests[21].clear = true;
        //FindAnyObjectByType<SpaceDoorOpen>().isOpen = true;
        Destroy(GameManager.Instance.gameObject);
        Destroy(UIManager.Instance.gameObject);
        Destroy(QuestManager.Instance.gameObject);
        SceneManager.LoadScene(LoadSceneNumber);
        enabled = false;
    }
    

    public void line()
    {
        QuestManager.Instance.quests[15].clear = true;
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("행성 HKSH로갈 준비가 되었나요??"));
        dialogTexts.Add(new DialogData("그럼 바로 이동하겠습니다"));
       
        Show(dialogTexts);
    }
}
