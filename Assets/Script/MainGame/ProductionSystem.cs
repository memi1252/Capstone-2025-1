using System;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using InventorySystem;
using UnityEditor;


public class ProductionSystem : MonoBehaviour
{
    [SerializeField] private string craftingTableName = "ProductionSlot";
    [SerializeField] private string resultSlotName = "Production";
    private InventoryItem[] craftingTableItems = new InventoryItem[9];

    private bool itemCrafted = false;

    private string[] itemNames;
    
    [Serializable]
    public struct recipe
    {
        public string name;
        public int[] itemIndex; // 필요한 재료의 인덱스
        public string[] requiredItems; // 각 위치에 필요한 아이템 이름
        public string resultItem; // 결과 아이템 이름
        public int resultAmount; // 결과 아이템 개수
        public bool isCreate;
    }

    public recipe[] recipes;
    
    
    private void Start()
    {

    }

    private void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            // if (InventoryController.instance.GetItem(craftingTableName, i) != null)
            // {
            //     craftingTableItems[i] = InventoryController.instance.GetItem(craftingTableName, i);
            // }
            craftingTableItems[i] = InventoryController.instance.GetItem(craftingTableName, i);
        }

        ItemCrafted();
    }
    
    private recipe makeItem = new recipe();
    void ItemCrafted()
    {
        int recipeCount = 0;
        bool isAnyRecipeMatched = false;
        // 제조법 맞느닞 확인후 아이템 생성
        foreach (var recipe in recipes)
        {
            if (!itemCrafted)
            {
                bool isRecipeMatch = true;
                for (int i = 0; i < recipe.itemIndex.Length; i++)
                {
                    int index = recipe.itemIndex[i];
                    string requiredItem = recipe.requiredItems[i];

                    if (craftingTableItems[index] == null ||
                        craftingTableItems[index].GetItemType() != requiredItem)
                    {
                        isRecipeMatch = false;
                        break;
                    }
                }

                if (isRecipeMatch)
                {
                    isAnyRecipeMatched = true;
                    if (!IsInventoryEmpty(resultSlotName))
                    {
                        InventoryItem existingItem = InventoryController.instance.GetItem(resultSlotName, 0);
                        if (existingItem != null)
                        {
                            InventoryController.instance.AddItem("MainSlot", existingItem.GetItemType(), 1);
                            InventoryController.instance.RemoveItem(resultSlotName, existingItem, 1);
                        }
                    }
                    makeItem = recipe;
                    
                    Debug.Log(makeItem.name);
                    InventoryController.instance.AddItem(resultSlotName, recipe.resultItem, recipe.resultAmount);
                    

                    itemCrafted = true;
                }
            }
        }

        if (itemCrafted)
        {
            if (makeItem.name != null)
            {
                makeItem.isCreate = true;
                for (int i = 0; i < recipes.Length; i++)
                {
                    if (recipes[i].name == makeItem.name)
                    {
                        recipes[i] = makeItem;
                        break;
                    }
                }
            }
        }
        
        // 결과 슬롯에서 아이템이 제거되었는지 확인
        if (itemCrafted && IsInventoryEmpty(resultSlotName))
        {
            itemCrafted = false;
            if (makeItem.name != null)
            {
                makeItem.isCreate = false;
                for (int i = 0; i < recipes.Length; i++)
                {
                    if (recipes[i].name == makeItem.name)
                    {
                        recipes[i] = makeItem;
                        break;
                    }
                }
            }
            if (makeItem.name == "NipperItem")
            {
                QuestManager.Instance.quests[4].clear = true;
                GameManager.Instance.nipperMake = true;
            }
            if (makeItem.name == "mongkeyspanerItem")
            {
                QuestManager.Instance.quests[12].clear = true;
                GameManager.Instance.mongkiMake = true;
            }

            if (makeItem.name == "fliterItem")
            {
                QuestManager.Instance.quests[19].clear = true;
                GameManager.Instance.fliterMake = true;
            }
            makeItem = new recipe();
            makeItem.isCreate = false;
            foreach (var recipe in recipes)
            {
                bool isRecipeMatch = true;
                for (int i = 0; i < recipe.itemIndex.Length; i++)
                {
                    int index = recipe.itemIndex[i];
                    string requiredItem = recipe.requiredItems[i];

                    if (craftingTableItems[index] == null ||
                        craftingTableItems[index].GetItemType() != requiredItem)
                    {
                        isRecipeMatch = false;
                        break;
                    }
                }

                if (isRecipeMatch)
                {
                    for (int i = 0; i < recipe.itemIndex.Length; i++)
                    {
                        int index = recipe.itemIndex[i];
                        if (craftingTableItems[index] != null)
                        {
                            InventoryController.instance.RemoveItem(craftingTableName, craftingTableItems[index], 1);
                        }
                    }
                    break;
                }
            }
        }

        //조합법이 다르면 삭제
        if (itemCrafted)
        {
            recipeCount = 0;
            foreach (var recipe in recipes)
            {
                //Debug.Log(recipes.Length);
                //Debug.Log(recipeCount);
                recipeCount++;
                bool isRecipeMatch = true;
                bool isfor = true;
                for (int i = 0; i < recipe.itemIndex.Length; i++)
                {
                    int index = recipe.itemIndex[i];
                    string requiredItem = recipe.requiredItems[i];
            
                    if (i == recipe.itemIndex.Length - 1)
                        isfor = false;
                    
                    if (craftingTableItems[index] == null ||
                        craftingTableItems[index].GetItemType() != requiredItem)
                    {
                        isRecipeMatch = false;
                        break;
                    }
                    else
                    {
                        if(!isfor)
                        recipeCount = 0;
                    }
                }

                if (recipe.isCreate)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < recipe.itemIndex.Length; j++)
                        {
                            if (recipe.itemIndex[j] == i)
                            {
                                i++;
                            }
                        }
                        if(i == 9)break;
                        if (craftingTableItems[i].GetItemType() != null)
                        {
                            Debug.Log(i);
                            DestroyResultItem();
                            recipeCount = 0;
                            break;
                        }
                    }
                }
                
                if(recipeCount == recipes.Length)
                {
                    DestroyResultItem();
                }
            }
        }
        
        
    }

    private void DestroyResultItem()
    {
        itemCrafted = false;
        if (makeItem.name != null)
        {
            makeItem.isCreate = false;
            for (int i = 0; i < recipes.Length; i++)
            {
                if (recipes[i].name == makeItem.name)
                {
                    recipes[i] = makeItem;
                    break;
                }
            }
        }
        makeItem = new recipe();
        makeItem.isCreate = false;
        InventoryItem removeItem = new InventoryItem(true);
        foreach (var recipee in recipes)
        {
            if (InventoryController.instance.GetItem(resultSlotName, 0) != null)
            {
                InventoryItem item = InventoryController.instance.GetItem(resultSlotName, 0);
                if (item.GetItemType() == recipee.resultItem)
                {
                    removeItem = item;
                    break;
                }
            }
        }
            
        if (removeItem.GetItemType() != null)
        {
            InventoryController.instance.RemoveItem(resultSlotName, removeItem, 1);
        }
    }
    
    
    public bool IsInventoryEmpty(string inventoryName)
    {
        
        Inventory inventory = InventoryController.instance.GetInventory(inventoryName);

        if (inventory == null)
        {
            Debug.LogError($"인벤토리 '{inventoryName}'를 찾을 수 없습니다.");
            return false;
        }
        
        foreach (InventoryItem item in inventory.GetList())
        {
            if (!item.GetIsNull()) 
            {
                return false;
            }
        }
        
        return true;
    }
}

