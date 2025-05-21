using System;
using InventorySystem;
using UnityEngine;

public class QuitslotItemSelete : MonoBehaviour
{
    private string inventoryName = "Quitslot";
    [SerializeField] private GameObject BatItem;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(InventoryController.instance.GetItem(inventoryName, 0));
            if (!InventoryController.instance.GetItem(inventoryName, 0).GetIsNull())
            {
                if (InventoryController.instance.GetItem(inventoryName, 0).GetItemType() == "Item")
                {
                    if (BatItem.activeInHierarchy)
                    {
                        BatItem.SetActive(false);
                    }
                    else
                    {
                        BatItem.SetActive(true);
                    }
                }
            }
        }
    }
}
