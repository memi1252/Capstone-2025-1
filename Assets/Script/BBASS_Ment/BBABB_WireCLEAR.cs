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
        
        Printer.SetActive(false);
    }

    public void Clear()
    {
        wire.GetComponent<MeshRenderer>().material = wireMaterial;
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("전선 연결이 완료되었습니다!"));
        dialogTexts.Add(new DialogData("오늘의 우주선에돌아가서 쉴까요?"));
        Show(dialogTexts);
        
    }
}
