using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WirePoint : MonoBehaviour
{
    public string wireName;
    public bool isWire;
    public  bool click;

    private TextMeshProUGUI text;
    public MeshRenderer meshRenderer;
    private WireConnections wireConnections;
    public bool inside;
    public Color originalColor;
    

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        meshRenderer = GetComponent<MeshRenderer>();
        wireConnections = GetComponentInParent<WireConnections>();
    }
    
    private void Start()
    {
        text.text = wireName;
        text.fontSize = 0.4f;
        text.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        meshRenderer.materials[4].color = Color.red;
        originalColor = meshRenderer.material.color;
    }

    private void Update()
    {
        if (inside && !click)
        {
            if (Input.GetMouseButtonDown(0))
            {
                click = true;
                if (isWire)
                {

                    meshRenderer.material.color = originalColor;
                }
                else
                {
                    meshRenderer.material.color = Color.yellow;
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
        if (other.CompareTag("MouseFollow"))
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MouseFollow"))
        {
            inside = false;
        }
    }
}
