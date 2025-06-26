using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class FirstINSpaceStation : MonoBehaviour
{
    private bool isPlay = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.dayContViewUI.DayCountPlay(1);
            QuestManager.Instance.quests[2].clear = true;
            GetComponent<Collider>().enabled = false;
            //gameObject.SetActive(false);
            StartCoroutine(BBASSMent());
            GameManager.Instance.spaceStationEntranceHelpUI.SetActive(false);
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

    IEnumerator BBASSMent()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData("우주정거장 탐색 결과 도구를 만들 재료는 충분합니다."));
        dialogTexts.Add(new DialogData("현재 필요한 재료는 철판, 나사, 볼트입니다."));
        dialogTexts.Add(new DialogData("피로도가 다 떨어지기 전에 모아서 우주선으로 돌아갑시다!"));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }
    
}
