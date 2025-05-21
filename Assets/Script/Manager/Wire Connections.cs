using System;
using UnityEngine;

public class WireConnections : MonoBehaviour
{
    public WirePoint[] wirePoints;
    public GameObject[] wires;
    public WirePoint wirePointA;
    public WirePoint wirePointB;
    public bool A;
    public bool B;


    private void Update()
    {
        if(wirePointA ==null || wirePointB == null) return;
        if (wirePointA.wireName == wirePointB.wireName)
        {
            Debug.Log("전선 연결");
            foreach (var wire in wires)
            {
                if(wire.name == "Wire" + wirePointB.wireName)
                {
                    wire.SetActive(true);
                }
            }
            wirePointA.meshRenderer.material.color = Color.green;
            wirePointB.meshRenderer.material.color = Color.green;
            wirePointA.isWire = true;
            wirePointB.isWire = true;
            wirePointB = null;
            wirePointA = null;
            A = false;
            B = false;
        }
        else
        {
            Debug.Log("전선 불일치");
            wirePointA.meshRenderer.material.color = Color.white;
            wirePointB.meshRenderer.material.color = Color.white;
            wirePointB = null;
            wirePointA = null;
            A = false;
            B = false;
        }

        foreach (var WirePoint in wirePoints)
        {
            if(!WirePoint.isWire)
            {
                break;
            }

            if (WirePoint == wirePoints[wirePoints.Length - 1])
            {
                WireManager.instance.Success();
            }
        }
    }
}
