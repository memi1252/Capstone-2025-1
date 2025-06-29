using System;
using System.Collections;
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
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
        GameManager.Instance.MouseCursor(true);
        GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        UIManager.Instance.QuitSlotUI.SetActive(false);
        UIManager.Instance.StastUI.SetActive(false);
        isMoveItem = true;
        GameManager.Instance.noESC = true;
        GameManager.Instance.noInventoryOpen = true;
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
                StartCoroutine(back());
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

    public void Backd()
    {
        StartCoroutine(back());
    }
    public IEnumerator back()
    {
        UIManager.Instance.itemDescriptionUI.SetActive(false);
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        isBackItem = true;
        isFrontItem = false;
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.tooltipUI.Hide();
        //GameManager.Instance.player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        // Camera.main.transform.GetComponent<Volume>().enabled = true;
        depthOfField.active = false;
        GameManager.Instance.noInventoryOpen = false;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.noESC = false;
    }
    
    public void Pickup()
    {
        if (!InventoryController.instance.InventoryFull("Quitslot", itemName))
        {
            InventoryController.instance.AddItem("Quitslot", itemName, itemCount);
            ItemAddInventory();
            if (!GameManager.Instance.isCardKet1)
            {
                if(itemName == "KeyCard1")
                    GameManager.Instance.isCardKet1 = true;
            }
            if (!GameManager.Instance.isCardKey2)
            { 
                if (itemName == "KeyCard2")
                {
                    GameManager.Instance.isCardKey2 = true;
                }
            }
            if (!GameManager.Instance.firstItemmat)
            {
                if (itemName == "NutItem") 
                { 
                    if(!GameManager.Instance.nipperMax[0]) GameManager.Instance.nipperMax[0] = true;
                }else if (itemName == "Screwitem")
                { 
                    if(!GameManager.Instance.nipperMax[1]) GameManager.Instance.nipperMax[1] = true;
                }else if (itemName == "IronPlateItem")
                { 
                    if(!GameManager.Instance.nipperMax[2]) GameManager.Instance.nipperMax[2] = true;
                }
            }else if(!GameManager.Instance.secondItemmat)
            {
                if (itemName == "NutItem")
                { 
                    if (!GameManager.Instance.mongkiMax[0])
                    { 
                        GameManager.Instance.mongkiCount[0]++; 
                        if (GameManager.Instance.mongkiCount[0] == GameManager.Instance.mongkiMaxCount[0])
                        { 
                            GameManager.Instance.mongkiMax[0] = true;
                        }
                    }
                    
                }else if (itemName == "Screwitem")
                {
                    if (!GameManager.Instance.mongkiMax[1])
                    {
                        GameManager.Instance.mongkiCount[1]++; 
                        if (GameManager.Instance.mongkiCount[1] == GameManager.Instance.mongkiMaxCount[1])
                        { 
                            GameManager.Instance.mongkiMax[1] = true;
                        }
                    }
                }else if (itemName == "IronPlateItem")
                {
                    if (!GameManager.Instance.mongkiMax[2])
                    { 
                        GameManager.Instance.mongkiCount[2]++; 
                        if (GameManager.Instance.mongkiCount[2] == GameManager.Instance.mongkiMaxCount[2])
                        { 
                            GameManager.Instance.mongkiMax[2] = true;
                        } 
                    }
                }
            }else if (!GameManager.Instance.thirdItemmat)
            {
                if (itemName == "NutItem")
                { 
                    if (!GameManager.Instance.fliterMax[0]) 
                    { 
                        GameManager.Instance.fliterCount[0]++; 
                        if (GameManager.Instance.fliterCount[0] == GameManager.Instance.fliterMaxCount[0]) 
                        {
                            GameManager.Instance.fliterMax[0] = true;
                        }
                    }
                            
                }else if (itemName == "Screwitem")
                { 
                    if (!GameManager.Instance.fliterMax[1])
                    { 
                        GameManager.Instance.fliterCount[1]++; 
                        if (GameManager.Instance.fliterCount[1] == GameManager.Instance.fliterMaxCount[1])
                        { 
                            GameManager.Instance.fliterMax[1] = true;
                        }
                    }
                }else if (itemName == "IronPlateItem")
                { 
                    if (!GameManager.Instance.fliterMax[2])
                    {
                        GameManager.Instance.fliterCount[2]++;
                        if (GameManager.Instance.fliterCount[2] == GameManager.Instance.fliterMaxCount[2])
                        { 
                            GameManager.Instance.fliterMax[2] = true;
                        }
                    }
                }
            }
        }
        else
        {
            if (!InventoryController.instance.InventoryFull("MainSlot",itemName))
            {
                InventoryController.instance.AddItem("MainSlot", itemName, itemCount);
                ItemAddInventory();
                if (!GameManager.Instance.isCardKet1)
                {
                    if(itemName == "KeyCard1")
                        GameManager.Instance.isCardKet1 = true;
                }

                if (!GameManager.Instance.isCardKey2)
                {
                    if (itemName == "KeyCard2")
                    {
                        GameManager.Instance.isCardKey2 = true;
                    }
                }
                if (!GameManager.Instance.firstItemmat)
                {
                    if (itemName == "NutItem")
                    {
                        if(!GameManager.Instance.nipperMax[0]) GameManager.Instance.nipperMax[0] = true;
                    }else if (itemName == "Screwitem")
                    {
                        if(!GameManager.Instance.nipperMax[1]) GameManager.Instance.nipperMax[1] = true;
                    }else if (itemName == "IronPlateItem")
                    {
                        if(!GameManager.Instance.nipperMax[2]) GameManager.Instance.nipperMax[2] = true;
                    }
                }else if(!GameManager.Instance.secondItemmat)
                {
                    if (itemName == "NutItem")
                    {
                        if (!GameManager.Instance.mongkiMax[0])
                        {
                            GameManager.Instance.mongkiCount[0]++;
                            if (GameManager.Instance.mongkiCount[0] == GameManager.Instance.mongkiMaxCount[0])
                            {
                                GameManager.Instance.mongkiMax[0] = true;
                            }
                        }
                            
                    }else if (itemName == "Screwitem")
                    {
                        if (!GameManager.Instance.mongkiMax[1])
                        {
                            GameManager.Instance.mongkiCount[1]++;
                            if (GameManager.Instance.mongkiCount[1] == GameManager.Instance.mongkiMaxCount[1])
                            {
                                GameManager.Instance.mongkiMax[1] = true;
                            }
                        }
                    }else if (itemName == "IronPlateItem")
                    {
                        if (!GameManager.Instance.mongkiMax[2])
                        {
                            GameManager.Instance.mongkiCount[2]++;
                            if (GameManager.Instance.mongkiCount[2] == GameManager.Instance.mongkiMaxCount[2])
                            {
                                GameManager.Instance.mongkiMax[2] = true;
                            }
                        }
                    }
                }else if (!GameManager.Instance.thirdItemmat)
                {
                    if (itemName == "NutItem")
                    {
                        if (!GameManager.Instance.fliterMax[0])
                        {
                            GameManager.Instance.fliterCount[0]++;
                            if (GameManager.Instance.fliterCount[0] == GameManager.Instance.fliterMaxCount[0])
                            {
                                GameManager.Instance.fliterMax[0] = true;
                            }
                        }
                            
                    }else if (itemName == "Screwitem")
                    {
                        if (!GameManager.Instance.fliterMax[1])
                        {
                            GameManager.Instance.fliterCount[1]++;
                            if (GameManager.Instance.fliterCount[1] == GameManager.Instance.fliterMaxCount[1])
                            {
                                GameManager.Instance.fliterMax[1] = true;
                            }
                        }
                    }else if (itemName == "IronPlateItem")
                    {
                        if (!GameManager.Instance.fliterMax[2])
                        {
                            GameManager.Instance.fliterCount[2]++;
                            if (GameManager.Instance.fliterCount[2] == GameManager.Instance.fliterMaxCount[2])
                            {
                                GameManager.Instance.fliterMax[2] = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Inventory Cannot Fit Item");
                StartCoroutine(back());
            }

        }
        
    }
    
    private void ItemAddInventory()
    {
        GameManager.Instance.player.ItemPickupSound.Play();
        GameManager.Instance.noESC = false;
        GameManager.Instance.noInventoryOpen = false;
        UIManager.Instance.itemDescriptionUI.SetActive(false);;
        UIManager.Instance.tooltipUI.Hide();
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.StastUI.SetActive(true);
        GameManager.Instance.MouseCursor(false);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        depthOfField.active = false;
        Destroy(gameObject);
    }
}
