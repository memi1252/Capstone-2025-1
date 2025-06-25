using System;
using TMPro;
using UnityEngine;

public class TooltipUI : UIBase
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        Hide();
    }

    public void SetText(string tooltipText)
    {
        text.text = tooltipText;
        Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
    
}
