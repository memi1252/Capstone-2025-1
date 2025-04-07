using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
        public float rayLength; // 레이 길이
        public List<GameObject> temSlot = new List<GameObject>();   // 슬롯 리스트
        public List<GameObject> prefabSlot = new List<GameObject>(); // 아이템 프리팹 리스트
        private int itemIndex = 0; // 인벤토리에 추가된 아이템 수
    
        void Start()
        {
            // 슬롯에 InventorySlot 스크립트가 있는지 확인하고 추가
            foreach (GameObject slot in temSlot)
            {
                if (!slot.GetComponent<InventorySlot>())
                {
                    slot.AddComponent<InventorySlot>();
                }
            }
        }
    
        void Update()
        {
            Debug.DrawRay(transform.position, Camera.main.transform.forward * rayLength, Color.red);
            RaycastHit hit;
    
            if (Input.GetKeyDown(KeyCode.F) && Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, rayLength, LayerMask.GetMask("Item")))
            {
                Debug.Log("아이템에 이름: " + hit.collider.name);
                AddToInventory(hit.collider.gameObject); //인벤토리에 아이템 추가
                Destroy(hit.collider.gameObject);
            }
        }
    
        public void AddToInventory(GameObject detectedItem)
        {
            if (temSlot == null || prefabSlot == null)
            {
                Debug.LogError("슬롯 또는 프리팹 리스트가 할당되지 않았습니다.");
                return;
            }
    
            bool slotFound = false;
            for (int i = 0; i < temSlot.Count; i++)
            {
                if (temSlot[i].transform.childCount == 0)
                {
                    if (itemIndex < prefabSlot.Count)
                    {
                        GameObject spawnedItem = Instantiate(prefabSlot[itemIndex], temSlot[i].transform);
                        spawnedItem.AddComponent<DraggableItem>(); // 드래그 가능 컴포넌트 추가
                        Debug.Log($"{itemIndex}번째 아이템 추가 완료: {spawnedItem.name}");
                        itemIndex++;
                        slotFound = true;
                        break;
                    }
                    else
                    {
                        Debug.LogWarning("모든 아이템을 획득했습니다.");
                        break;
                    }
                }
            }
            if (!slotFound) Debug.Log("인벤토리가 가득 찼습니다.");
        }
}
