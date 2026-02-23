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

        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story67")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story68")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story69")));
        Show(dialogTexts);
        
    }
}
