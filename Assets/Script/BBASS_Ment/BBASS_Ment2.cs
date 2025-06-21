using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;

public class BBASS_Ment2 : BBASS_MentBASE
{
    public bool play = false;
    private Quaternion originalRotation;

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
        originalRotation = transform.rotation;
        Vector3 targetRotation = GameManager.Instance.player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetRotation);
        while (transform.rotation.eulerAngles != rotation.eulerAngles)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100f* Time.deltaTime);
            yield return null;
        }
        yield return StartCoroutine(base.PrintDialogList(dataList));
        while (transform.rotation.eulerAngles != originalRotation.eulerAngles)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, 100f* Time.deltaTime);
            yield return null;
        }
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        FindAnyObjectByType<SpaceDoorOpen>().isOpen = true;
        
        play = false;
        Printer.SetActive(false);
        FindAnyObjectByType<SpaceShip>().isDoorFront = true;
        enabled = false;
    }
    

    public void line()
    {
            var dialogTexts = new List<DialogData>();
            dialogTexts.Add(new DialogData("무사히 우주정거장에 도착했습니다."));
            dialogTexts.Add(new DialogData("일단 우주정거장으로 가봅시다."));
            Show(dialogTexts);
        
    }
}
