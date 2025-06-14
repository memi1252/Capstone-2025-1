using System;
using InventorySystem;
using UnityEngine;

public class QuitslotItemSelect : MonoBehaviour
{
    private string inventoryName = "Quitslot";
    [SerializeField] private GameObject[] HandItem; 
    private GameObject currentHandItem = null;
    private int currentHandItemIndex = -1;
    private GameObject currentSlot;

    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleSlotItem(0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleSlotItem(1);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandleSlotItem(2);
        }else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HandleSlotItem(3);
        }
        
        if (currentHandItem != null)
        {
            for(int i = 0; i < 4; i++)
            {
                var item = InventoryController.instance.GetItem(inventoryName, i);
                if(item != null)
                {
                    if (item.GetItemType() == currentHandItem.name)
                    {
                        currentHandItemIndex = i;
                    }
                }
            }

            if (currentHandItemIndex != -1)
            {
                var item = InventoryController.instance.GetItem(inventoryName, currentHandItemIndex);

                if (item != null)
                {
                    if (item.GetItemType() != currentHandItem.name)
                    {
                        currentHandItem.SetActive(false);
                        currentHandItem = null;
                        currentSlot.transform.GetChild(1).gameObject.SetActive(false);
                        currentSlot = null;
                        currentHandItemIndex = -1;
                    }
                }
            }
        }
        
    }

    private void HandleSlotItem(int slotIndex)
    {
        var item = InventoryController.instance.GetItem(inventoryName, slotIndex);

        if (item != null && !item.GetIsNull())
        {
            foreach (var items in HandItem)
            {
                if (item.GetItemType() == items.name)
                {
                    if (currentHandItem == null)
                    {
                        items.SetActive(true);
                        currentSlot = GameObject.Find(inventoryName).GetComponent<InventoryUIManager>().GetSlot(slotIndex).transform.GetChild(0).gameObject;
                        currentSlot.transform.GetChild(1).gameObject.SetActive(true);
                        currentHandItem = items;
                    }
                    else
                    {
                        if (currentHandItem != items)
                        {
                            currentHandItem.SetActive(false);
                            items.SetActive(true);
                            currentSlot.transform.GetChild(1).gameObject.SetActive(false);
                            currentSlot = GameObject.Find(inventoryName).GetComponent<InventoryUIManager>().GetSlot(slotIndex).transform.GetChild(0).gameObject;
                            currentSlot.transform.GetChild(1).gameObject.SetActive(true);
                            currentHandItem = items;
                        }
                        else
                        {
                            currentHandItem.SetActive(false);
                            currentSlot.transform.GetChild(1).gameObject.SetActive(false);
                            currentSlot = null;
                            currentHandItem = null;
                        }
                    }
                    
                }
            }
            
        }
    }
    
    
}