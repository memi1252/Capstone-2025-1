using System;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : UIBase
{
    [SerializeField] private List<WireConnectionUI> wires; // 전선 리스트
    public bool isDragging = false; // 드래그 상태 플래그
    public bool clear;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (!clear)
        {
            CheckAllConnections();
        }
    }

    public void CheckAllConnections()
    {
        bool allCorrect = true;

        foreach (var wire in wires)
        {
            if (!wire.IsCorrectConnection)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            clear = true;
            //임시
            Hide();
            Debug.Log("모든 전선이 올바르게 연결되었습니다!");
        }
    }
    
    override public void Show()
    {
        base.Show();
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
    }

    public override void Hide()
    {
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        ResetWires();
        base.Hide();
    }
    
    public void ResetWires()
    {
        clear = false;
        foreach (var wire in wires)
        {
            wire.ResetWireState();
        }
        Debug.Log("전선 상태가 초기화되었습니다.");
    }
}