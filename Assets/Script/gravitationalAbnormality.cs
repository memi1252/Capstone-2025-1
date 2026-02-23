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
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story51")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story52")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story53")));
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
