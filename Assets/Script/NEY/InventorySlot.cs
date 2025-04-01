using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem != null)
        {
            // 드롭된 슬롯이 비어있으면 그냥 이동
            if (transform.childCount == 0)
            {
                draggableItem.parentAfterDrag = transform;
            }
            // 드롭된 슬롯에 자식이 있으면 교환
            else if (transform.childCount > 0)
            {
                Transform existingItem = transform.GetChild(0);
                existingItem.SetParent(draggableItem.parentBeforeDrag);
                existingItem.localPosition = Vector3.zero; // 위치 초기화
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}