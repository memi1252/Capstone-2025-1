using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class BBABB_ReplacingCLEAR : BBASS_MentBASE
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

        dialogTexts.Add(new DialogData("추진 제어판가 수리되었습니다."));
        dialogTexts.Add(new DialogData("내일 생명 유지장치만 수리하면 될꺼 같습니다."));
        dialogTexts.Add(new DialogData("오늘은 이만 돌아가 휴식을 취합시다."));
        Show(dialogTexts);
        
    }
}
