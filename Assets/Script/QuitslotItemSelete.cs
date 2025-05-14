using System;
using InventorySystem;
using UnityEngine;

public class QuitslotItemSelete : MonoBehaviour
{
    private string inventoryName = "Quitslot";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(InventoryController.instance.GetItem(inventoryName, 0));

        }
    }
}
