using System;
using InventorySystem;
using UnityEngine;

public class QuitslotItemSelect : MonoBehaviour
{
    private string inventoryName = "Quitslot";
    [SerializeField] private GameObject[] HandItem; 
    public GameObject currentHandItem = null;
    private int currentHandItemIndex = -1;
    private GameObject currentSlot;

    private bool TakeKeycode = false;
    
    private void Update()
    {
        if (currentHandItem == HandItem[5])
        {
            if (!TakeKeycode)
            {
                GameManager.Instance.player.haveKeycode.Add("1234");
                TakeKeycode = true;
            }
        }else if (currentHandItem == HandItem[6])
        {
            if (!TakeKeycode)
            {
                GameManager.Instance.player.haveKeycode.Add("1252");
                TakeKeycode = true;
            }
        }
        else
        {
            TakeKeycode = false;
            GameManager.Instance.player.haveKeycode.Clear();
        }
        
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
                        currentHandItem = items;
                        items.SetActive(true);
                        currentSlot = GameObject.Find(inventoryName).GetComponent<InventoryUIManager>().GetSlot(slotIndex).transform.GetChild(0).gameObject;
                        currentSlot.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        if (currentHandItem != items)
                        {
                            currentHandItem.SetActive(false);
                            currentHandItem = items;
                            items.SetActive(true);
                            currentSlot.transform.GetChild(1).gameObject.SetActive(false);
                            currentSlot = GameObject.Find(inventoryName).GetComponent<InventoryUIManager>().GetSlot(slotIndex).transform.GetChild(0).gameObject;
                            currentSlot.transform.GetChild(1).gameObject.SetActive(true);
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