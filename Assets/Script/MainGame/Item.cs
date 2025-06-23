using System;
using InventorySystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    public bool outline = false;
    private GameObject Item;
    
    private Volume volume;
    private DepthOfField depthOfField;

    public void frontitem(GameObject item)
    {
        volume = Camera.main.transform.GetComponent<Volume>();
        volume.profile.TryGet(out depthOfField);
        Item = item;
        origiPos = transform.position;
        origiRot = transform.rotation;
        isFrontItem = true;
        Item.transform.GetComponent<Collider>().isTrigger = true;
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
        GameManager.Instance.MouseCursor(true);
        GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        UIManager.Instance.QuitSlotUI.SetActive(false);
        UIManager.Instance.StastUI.SetActive(false);
        isMoveItem = true;
        GameManager.Instance.isItemPickUp = true;
    }

    private void Update()
    {
        if (outline)
        {
            //GetComponent<Renderer>().material.SetColor("_EmissionColor", 0.0f * Color.white);
            //outline = false;
        }
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
            //UIManager.Instance.InvneoryUI.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("itemviewPos").transform.position, itemMoveSpeed * Time.deltaTime);
            if (transform.position == GameObject.FindGameObjectWithTag("itemviewPos").transform.position)
            {
                UIManager.Instance.itemDescriptionUI.SetActive(true);
                UIManager.Instance.itemDescriptionUI.GetComponent<ItemDescriptionUI>().SetItem(this);
                isMoveItem = false;
                isRotateItem = true;
                //GameManager.Instance.player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                // Camera.main.transform.GetComponent<Volume>().enabled = false;
                depthOfField.active = true;
                
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
                UIManager.Instance.StastUI.SetActive(true);
                Item.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Item.transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                Item.transform.GetComponent<Collider>().isTrigger = false;
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
        UIManager.Instance.itemDescriptionUI.SetActive(false);
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        GameManager.Instance.isItemPickUp= false;
        
        isBackItem = true;
        isFrontItem = false;
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.tooltipUI.Hide();
        //GameManager.Instance.player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        // Camera.main.transform.GetComponent<Volume>().enabled = true;
        depthOfField.active = false;
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
        UIManager.Instance.itemDescriptionUI.SetActive(false);;
        UIManager.Instance.tooltipUI.Hide();
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.StastUI.SetActive(true);
        GameManager.Instance.isItemPickUp= false;
        GameManager.Instance.MouseCursor(false);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        depthOfField.active = false;
        Destroy(gameObject);
    }
}
