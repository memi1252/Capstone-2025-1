using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireConnectionUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform startPoint; 
    [SerializeField] private RectTransform endPoint;   
    [SerializeField] private RectTransform targetPoint; 
    [SerializeField] private Image wireImage; 
    private bool isConnected = false;

    public bool IsCorrectConnection { get; private set; } = false; 

    private void Start()
    {
        wireImage.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        if (isConnected) return;
        wireImage.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)wireImage.transform.parent,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePosition
        );
        endPoint.anchoredPosition = localMousePosition;
        
        UpdateWire();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        
        float distance = Vector2.Distance(endPoint.anchoredPosition, targetPoint.anchoredPosition);
        if (distance < 50f)
        {
            isConnected = true;
            IsCorrectConnection = true;
            endPoint.anchoredPosition = targetPoint.anchoredPosition;
            UpdateWire();
            Debug.Log("전선 연결 성공");
        }
        else
        {
            ResetWire();
            Debug.Log("전선 연결 실패");
        }
    }

    private void UpdateWire()
    {
        Vector2 direction = endPoint.anchoredPosition - startPoint.anchoredPosition;
        float distance = direction.magnitude;
        wireImage.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, wireImage.GetComponent<RectTransform>().sizeDelta.y);
        wireImage.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        wireImage.GetComponent<RectTransform>().anchoredPosition = startPoint.anchoredPosition;
        wireImage.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void ResetWire()
    {
        endPoint.anchoredPosition = startPoint.anchoredPosition;
        UpdateWire();
        wireImage.enabled = false;
    }
    public void ResetWireState()
    {
        isConnected = false;
        IsCorrectConnection = false;
        endPoint.anchoredPosition = startPoint.anchoredPosition;
        wireImage.enabled = false;
        Debug.Log("전선이 초기화되었습니다.");
    }
    
}