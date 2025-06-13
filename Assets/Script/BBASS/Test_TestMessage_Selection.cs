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

        dialogTexts.Add(new DialogData("안녕하세요 저는 'BBASS' 입니다"));
        dialogTexts.Add(new DialogData("조금 힘들고 지쳐도"));
        dialogTexts.Add(new DialogData("발표 들어주시면 감사하겠습니다"));
        dialogTexts.Add(new DialogData("이상 'BBASS' 였습니다"));

        DialogManager.Show(dialogTexts);
    }
}
