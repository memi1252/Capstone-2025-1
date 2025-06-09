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
            wirePointA.meshRenderer.materials[4].color = Color.green;
            wirePointB.meshRenderer.materials[4].color = Color.green;
            wirePointA.meshRenderer.material.color = wirePointA.originalColor;
            wirePointB.meshRenderer.material.color = wirePointB.originalColor;
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
            wirePointA.click = false;
            wirePointB.click = false;
            wirePointA.meshRenderer.materials[4].color = Color.red;
            wirePointB.meshRenderer.materials[4].color = Color.red;
            wirePointA.meshRenderer.material.color = wirePointA.originalColor;
            wirePointB.meshRenderer.material.color = wirePointB.originalColor;
            wirePointB = null;
            wirePointA = null;
            A = false;
            B = false;
            WireManager.instance.count--;
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
