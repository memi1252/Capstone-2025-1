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
        dialogTexts.Add(new DialogData("아니 근데     진짜"));
        dialogTexts.Add(new DialogData("알로알로 T.H.U.N.D.E.R 알로알로"));
        dialogTexts.Add(new DialogData("떠올라 마치 번개처럼 번쩍"));
        dialogTexts.Add(new DialogData("알로알로 T.H.U.N.D.E.R 알로알로"));

        DialogManager.Show(dialogTexts);
    }
}
