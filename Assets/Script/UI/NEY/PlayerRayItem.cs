using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRayItem : MonoBehaviour
{
    public float RayLength;
    public List<GameObject> Slot = new List<GameObject>();
    private int itemIndex = 0;
    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Camera.main.transform.forward * RayLength, Color.red);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.F) && Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, RayLength, LayerMask.GetMask("Item")))
        {
            AddToInventory(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
        }
    }

    public void AddToInventory(GameObject detectedItem)
    {
        if (Slot == null)
        {
            Debug.Log("슬롯이 할당되지 않음");
        }

        for (int i = 0; i < Slot.Count; i++)
        {
            /*Slot[i].transform.childCount == 0;*/
        }
    }
}
