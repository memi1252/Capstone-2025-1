using System;
using InventorySystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] private int itemCount;
    [SerializeField] private float itemMoveSpeed =1;
    [SerializeField] private float mouseSensitivity = 100;

    private Vector3 origiPos;
    private Quaternion origiRot;
    
    public bool isFrontItem = false;
    public bool isMoveItem = false;
    public bool isBackItem = false;
    public bool isRotateItem = false;
    private GameObject Item;

    public void frontitem(GameObject item)
    {
        Item = item;
        origiPos = transform.position;
        origiRot = transform.rotation;
        isFrontItem = true;
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
        GameManager.Instance.MouseCursor(true);
        GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        UIManager.Instance.QuitSlotUI.SetActive(false);
        UIManager.Instance.MinMapUI.SetActive(false);
        UIManager.Instance.StastUI.SetActive(false);
        isMoveItem = true;
        GameManager.Instance.isItemPickUp = true;
    }

    private void Update()
    {
        if (isFrontItem)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                back();
            }
            transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (isMoveItem)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.player.transform.GetChild(1).position, itemMoveSpeed * Time.deltaTime);
            if (transform.position == GameManager.Instance.player.transform.GetChild(1).position)
            {
                GameManager.Instance.player.transform.GetChild(2).gameObject.SetActive(true);
                Camera.main.transform.GetComponent<Volume>().enabled = false;
                UIManager.Instance.itemDescriptionUI.Show();
                UIManager.Instance.itemDescriptionUI.SetItem(this);
                isMoveItem = false;
                isRotateItem = true;
            }
        }

        if (isBackItem)
        {
            transform.position = Vector3.MoveTowards(transform.position, origiPos, itemMoveSpeed * Time.deltaTime);
            if (origiPos == transform.position)
            {
                isBackItem = false;
                isRotateItem = false;
                transform.position = origiPos;
                transform.rotation = origiRot;
                UIManager.Instance.QuitSlotUI.SetActive(true);
                UIManager.Instance.MinMapUI.SetActive(true);
                UIManager.Instance.StastUI.SetActive(true);
            }
        }
        
        if (isRotateItem)
        {
            transform.GetComponent<ObjectRotation>().enabled = true;
        }
        else
        {
            transform.GetComponent<ObjectRotation>().enabled = false;
        }
    }

    public void back()
    {
        isBackItem = true;
        isFrontItem = false;
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        GameManager.Instance.player.transform.GetChild(2).gameObject.SetActive(false);
        Camera.main.transform.GetComponent<Volume>().enabled = true;
        GameManager.Instance.isItemPickUp= false;
        UIManager.Instance.itemDescriptionUI.Hide();
    }
    
    public void Pickup()
    {
        if (!InventoryController.instance.InventoryFull("Quitslot", itemName))
        {
            InventoryController.instance.AddItem("Quitslot", itemName, itemCount);
            ItemAddInventory();
        }
        else
        {
            if (!InventoryController.instance.InventoryFull("MainSlot",itemName))
            {
                InventoryController.instance.AddItem("MainSlot", itemName, itemCount);
                ItemAddInventory();
                
            }
            else
            {
                Debug.Log("Inventory Cannot Fit Item");
                back();
            }

        }
        
    }
    
    private void ItemAddInventory()
    {
        GameManager.Instance.MouseCursor(false);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.MinMapUI.SetActive(true);
        UIManager.Instance.StastUI.SetActive(true);
        Camera.main.transform.GetComponent<Volume>().enabled = true;
        GameManager.Instance.player.transform.GetChild(2).gameObject.SetActive(false);
        UIManager.Instance.itemDescriptionUI.Hide();
        GameManager.Instance.isItemPickUp= false;
        Destroy(Item);
    }
}
