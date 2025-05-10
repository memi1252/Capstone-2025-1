using System;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using Unity.VisualScripting;
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
        public int[] itemIndex; // 필요한 재료의 인덱스
        public string[] requiredItems; // 각 위치에 필요한 아이템 이름
        public string resultItem; // 결과 아이템 이름
        public int resultAmount; // 결과 아이템 개수
        public bool iscrafted; // 조합 여부
    }

    [SerializeField] private recipe[] recipes;
    
    private void Start()
    {

    }

    private void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (InventoryController.instance.GetItem(craftingTableName, i) != null)
            {
                craftingTableItems[i] = InventoryController.instance.GetItem(craftingTableName, i);
            }
        }

        ItemCrafted();
    }

    void ItemCrafted()
    {
        bool isAnyRecipeMatched = false;
        recipe matchedRecipe = new recipe();
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
                    matchedRecipe = recipe;
                    InventoryController.instance.AddItem(resultSlotName, recipe.resultItem, recipe.resultAmount);
                    

                    itemCrafted = true;
                }
            }
        }

        if (itemCrafted) // 결과 스롯에 어떤 아이템이 만들어졌는지 확인
        {
            matchedRecipe.iscrafted = true;
        }
        
        // 결과 슬롯에서 아이템이 제거되었는지 확인
        if (itemCrafted && IsInventoryEmpty(resultSlotName))
        {
            itemCrafted = false;
            
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
                        matchedRecipe = recipe;//
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
                    matchedRecipe.iscrafted = false;//
                    break;
                }
            }
        }

        if (itemCrafted)
        {
            
            // int count = 0;
            // foreach (var recipe in recipes)
            // {
            //     
            //     Debug.Log(recipes.Length);
            //     Debug.Log(count);
            //     bool isRecipeMatch = true;
            //     count++;
            //     for (int i = 0; i < recipe.itemIndex.Length; i++)
            //     {
            //         int index = recipe.itemIndex[i];
            //         string requiredItem = recipe.requiredItems[i];
            //
            //         if (craftingTableItems[index] == null ||
            //             craftingTableItems[index].GetItemType() != requiredItem)
            //         {
            //             break;
            //         }
            //
            //     }
            //     if(count <= recipes.Length)
            //     {
            //         itemCrafted = false;
            //         InventoryItem removeItem = new InventoryItem(true);
            //         foreach (var recipee in recipes)
            //         {
            //             if (InventoryController.instance.GetItem(resultSlotName, 0) != null)
            //             {
            //                 InventoryItem item = InventoryController.instance.GetItem(resultSlotName, 0);
            //                 if (item.GetItemType() == recipee.resultItem)
            //                 {
            //                     removeItem = item;
            //                     break;
            //                 }
            //             }
            //         }
            //
            //         if (removeItem.GetItemType() != null)
            //         {
            //             InventoryController.instance.RemoveItem(resultSlotName, removeItem, 1);
            //         }
            //     }
            // }
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

