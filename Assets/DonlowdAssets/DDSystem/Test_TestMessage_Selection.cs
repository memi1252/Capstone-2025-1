using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class Test_TestMessage_Selection : MonoBehaviour
{
    public Test_DialogManager DialogManager;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("이거진짜된다"));
        dialogTexts.Add(new DialogData("승철정한지수준휘순영원우지훈"));
        dialogTexts.Add(new DialogData("명호민규석민승관한솔찬"));
        dialogTexts.Add(new DialogData("변지우바보바보바보"));
        dialogTexts.Add(new DialogData("이바보바보바보"));

        DialogManager.Show(dialogTexts);
    }
}
