using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    [SerializeField] private List<WireConnectionUI> wires; // 전선 리스트
    public bool isDragging = false; // 드래그 상태 플래그
    public bool clear;

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
            Debug.Log("모든 전선이 올바르게 연결되었습니다!");
        }
    }
}