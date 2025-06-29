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
        dialogTexts.Add(new DialogData("우주정거장 스캔 결과 도구를 만들 재료는 충분합니다"));
        dialogTexts.Add(new DialogData("현재 필요한 재료는 철판, 나사, 볼트입니다"));
        dialogTexts.Add(new DialogData("피로도와 산소가 다 떨어지기 전에 모아서 우주선으로 복귀해주세요"));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }

    IEnumerator BBASSMent2()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData("스캔 결과 좀 더 안쪽으로 들어가야 할 것 같습니다"));
        dialogTexts.Add(new DialogData("만약 잠긴문이 있다면 카드키를 찾아 열어야 합니다"));
        dialogTexts.Add(new DialogData("잠긴 문이 보이면"));
        dialogTexts.Add(new DialogData("카드키를 찾아서 우주정거장을 탐색해주세요"));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }

    IEnumerator BBASSMent3()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData("스캔 결과 재료가 어제보다 더 깊숙히 있습니다"));
        dialogTexts.Add(new DialogData("아이템을 찾으려면 잠긴 문을 지나가야 합니다"));
        dialogTexts.Add(new DialogData("카드키를 찾아 잠긴 문을 열고 재료를 구해주세요"));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }
    
}
