using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class gravitationalAbnormality : BBASS_MentBASE
{
    private bool iddddd = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!iddddd)
            {
                StartCoroutine(BBASSMentFrist());
                iddddd = true;
            }
            
        }
    }
    
    IEnumerator BBASSMentFrist()
    {
        yield return new WaitForSeconds(2f);
        var dialogTexts = new List<DialogData>();
        isPlay = true;
        dialogTexts.Add(new DialogData("삐빅 여기서 부터 몇몇의 방들이"));
        dialogTexts.Add(new DialogData("중력 이상으로 인해 중력이 약해지거나 몸을 움직이기 힘들어질수있습니다."));
        dialogTexts.Add(new DialogData("조심하시길 바람니다"));
        GameManager.Instance.BBASS.Show(dialogTexts);
    }

    private void Update()
    {
        if (!GameManager.Instance.BBASS.isPlay && isPlay)
        {
            GameManager.Instance.BBASS.Printer.SetActive(false);
            isPlay = false;
            enabled = false;
            
        }
    }
}
