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
    
    private AudioSource audioSource;
    public AudioClip[] clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if(wirePointA ==null || wirePointB == null) return;
        if (wirePointA.wireName == wirePointB.wireName)
        {
            Debug.Log("전선 연결");
            audioSource.clip = clip[0];
            audioSource.Play();
            foreach (var wire in wires)
            {
                if(wire.name == "Wire" + wirePointB.wireName)
                {
                    wire.SetActive(true);
                }
            }
            wirePointA.meshRenderer.materials[4].color = Color.green;
            wirePointB.meshRenderer.materials[4].color = Color.green;
            var material = wirePointA.meshRenderer.materials[4];
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.green * 2f);
            material = wirePointB.meshRenderer.materials[4];
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.green * 2f);
            
            
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
            audioSource.clip = clip[1];
            audioSource.Play();
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
