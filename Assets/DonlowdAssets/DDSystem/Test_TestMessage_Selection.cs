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
        dialogTexts.Add(new DialogData("아니 근데     진짜짜"));
        dialogTexts.Add(new DialogData("이건 아니잖아아ㅏㅏ"));

        DialogManager.Show(dialogTexts);
    }
}
