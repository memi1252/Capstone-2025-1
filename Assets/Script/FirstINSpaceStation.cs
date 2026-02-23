using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class FirstINSpaceStation : MonoBehaviour
{
    private bool isPlay = false;
    private bool isFirst = false;
    private bool isSecond = false;
    private bool isThird = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFirst)
            {
                UIManager.Instance.dayContViewUI.DayCountPlay(1);
                QuestManager.Instance.quests[2].clear = true;
                GetComponent<Collider>().enabled = false;
                //gameObject.SetActive(false);
                UIManager.Instance.StastUI.GetComponentInChildren<OtherUIvalue>().isFat = true;
                StartCoroutine(BBASSMentFrist());
                GameManager.Instance.spaceStationEntranceHelpUI.SetActive(false);
                isFirst = true;
            }
            else
            {
                if (!isSecond)
                {
                    StartCoroutine(BBASSMent2());
                    QuestManager.Instance.quests[8].clear = true;
                    GetComponent<Collider>().enabled = false;
                    isSecond = true;
                }
                else
                {
                    if (!isThird)
                    {
                        StartCoroutine(BBASSMent3());
                        QuestManager.Instance.quests[16].clear = true;
                        GetComponent<Collider>().enabled = false;
                        isThird = true;
                    }
                    else
                    {
                        
                    }
                }
            }
            
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.BBASS.isPlay && isPlay)
        {
            GameManager.Instance.BBASS.Printer.SetActive(false);
            isPlay = false;
        }
    }

    IEnumerator BBASSMentFrist()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story21")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story22")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story23")));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }

    IEnumerator BBASSMent2()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story24")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story25")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story26")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story27")));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }

    IEnumerator BBASSMent3()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story28")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story29")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story30")));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }
    
}
