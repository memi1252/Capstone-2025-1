using InventorySystem;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemCount;
    public void Pickup(GameObject item)
    {
        if (!InventoryController.instance.InventoryFull("Quitslot", itemName))
        {
            InventoryController.instance.AddItem("Quitslot", itemName, itemCount);
            Destroy(item);

        }
        else
        {
            if (!InventoryController.instance.InventoryFull("MainSlot",itemName))
            {
                InventoryController.instance.AddItem("MainSlot", itemName, itemCount);
                Destroy(item);

            }
            else
            {
                Debug.Log("Inventory Cannot Fit Item");
            }

        }
    }
}
