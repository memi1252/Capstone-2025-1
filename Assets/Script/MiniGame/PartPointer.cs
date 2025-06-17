using System;
using UnityEngine;

public class PartPointer : MonoBehaviour
{
    public bool inside;
    public ObjectFollow objectFollow;
    public BoxCollider boxCollider;
    public bool take = false;
    public InstallationPartPointer insPartPointer;
    public int value;
    public GameObject particle;

    private void Awake()
    {
        objectFollow = GetComponent<ObjectFollow>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (inside && !take && ReplacingPartsManager.instance.takePart == null)
            {
                Debug.Log("ss");
                take = true;
                insPartPointer.enabled = true;
                ReplacingPartsManager.instance.takePart = this;
                objectFollow.enabled = true;
                boxCollider.enabled = false;
                insPartPointer.take = false;
                particle.SetActive(false);
                insPartPointer = null;
                
            }
        }

        if (insPartPointer != null)
        {
            if (value != insPartPointer.value)
            {
                particle.SetActive(true);
            }
            else
            {
                particle.SetActive(false);
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
