using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WirePoint : MonoBehaviour
{
    public string wireName;
    public bool isWire;

    private TMP_Text text;
    public MeshRenderer meshRenderer;
    private WireConnections wireConnections;
    private bool inside;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        meshRenderer = GetComponent<MeshRenderer>();
        wireConnections = GetComponentInParent<WireConnections>();
    }
    
    private void Start()
    {
        text.text = wireName;
    }

    private void Update()
    {
        if (inside)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isWire)
                {
                    meshRenderer.material.color = Color.green;
                }
                else
                {
                    meshRenderer.material.color = Color.red;
                    if (!wireConnections.A)
                    {
                        wireConnections.A = true;
                        wireConnections.wirePointA = this;
                    }
                    else
                    {
                        if (!wireConnections.B)
                        {
                            wireConnections.B = true;
                            wireConnections.wirePointB = this;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CutObject")
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "CutObject")
        {
            inside = false;
        }
    }
}
