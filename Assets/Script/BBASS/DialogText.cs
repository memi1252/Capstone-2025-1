using UnityEngine;
using System.Collections.Generic;
using Doublsb.Dialog;

public class DialogText : MonoBehaviour
{
    public B_DialogManager B_DM;
    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        var text1 = new DialogData("아이우에오");
        var text2 = new DialogData("사시스세소");

        dialogTexts.Add(text1);
        dialogTexts.Add(text2);

        B_DM.Show(dialogTexts);
    }
}
