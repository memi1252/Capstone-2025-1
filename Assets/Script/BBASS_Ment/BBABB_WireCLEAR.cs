using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class BBABB_WireCLEAR : BBASS_MentBASE
{
    public GameObject wire;
    public Material wireMaterial;
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        yield return StartCoroutine(base.PrintDialogList(dataList));

        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        FindAnyObjectByType<BED>().goodNight = true;
        Printer.SetActive(false);
    }

    public void Clear()
    {
        wire.GetComponent<MeshRenderer>().material = wireMaterial;
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("전력 분배기가 수리되었습니다."));
        dialogTexts.Add(new DialogData("수리하느라 수고하셧습니다."));
        dialogTexts.Add(new DialogData("오늘은 여기까지 수리하고 휴식을 취합시다."));
        Show(dialogTexts);
        
    }
}
