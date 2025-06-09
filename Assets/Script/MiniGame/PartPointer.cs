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
        if (inside && !take)
        {
            Debug.Log("inside");
            if (Input.GetMouseButtonDown(0) && ReplacingPartsManager.instance.takePart == null)
            {
                Debug.Log("ss");
                take = true;
                ReplacingPartsManager.instance.takePart = this;
                objectFollow.enabled = true;
                boxCollider.enabled = false;
                insPartPointer.take = false;
                insPartPointer = null;
                
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
