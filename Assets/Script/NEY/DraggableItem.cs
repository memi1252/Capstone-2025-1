using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentBeforeDrag;
    public Transform parentAfterDrag;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>(); // CanvasGroup 추가
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root); // Canvas 최상단으로 이동
        transform.SetAsLastSibling(); // 다른 UI 위에 보이게
        canvasGroup.blocksRaycasts = false; // 드래그 중 클릭 방지
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // 마우스 따라 이동
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag == null) // 드롭 실패 시 원래 자리로
        {
            transform.SetParent(parentBeforeDrag);
        }
        else // 드롭 성공 시 새 부모로 이동
        {
            transform.SetParent(parentAfterDrag);
        }
        transform.localPosition = Vector3.zero; // 슬롯 중앙에 맞추기
        canvasGroup.blocksRaycasts = true;
        parentAfterDrag = null; // 초기화
    }
}