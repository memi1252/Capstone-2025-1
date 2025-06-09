using UnityEngine;

public class InstallationPartPointer : MonoBehaviour
{
   [SerializeField] private bool inside;
    public bool take;
    public int value;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (inside && ReplacingPartsManager.instance.takePart != null && ReplacingPartsManager.instance.takePart.take && !take)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Taking part");
                ReplacingPartsManager.instance.takePart.objectFollow.enabled = false;
                ReplacingPartsManager.instance.takePart.transform.position = transform.position;
                ReplacingPartsManager.instance.takePart.boxCollider.enabled = true;
                ReplacingPartsManager.instance.takePart.insPartPointer = this;
                Invoke("takePartReset", 0.03f);
            }
        }
    }
    
    private void takePartReset()
    {
        ReplacingPartsManager.instance.takePart.take = false;
        take = true;
        ReplacingPartsManager.instance.takePart = null;
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
