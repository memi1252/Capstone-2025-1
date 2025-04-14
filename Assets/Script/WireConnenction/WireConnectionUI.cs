using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireConnectionUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform startPoint; // 전선 시작점
    [SerializeField] private RectTransform endPoint;   // 전선 끝��
    [SerializeField] private RectTransform targetPoint; // 올바른 연결 지점
    [SerializeField] private Image wireImage; // 전선을 나타내는 UI 이미지
    private bool isConnected = false;

    public bool IsCorrectConnection { get; private set; } = false; // 올바른 연결 여부

    private void Start()
    {
        // 전선 초기화
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

        // 마우스 위치를 따라 전선 끝점 이동
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)wireImage.transform.parent,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePosition
        );
        endPoint.anchoredPosition = localMousePosition;

        // 전선 이미지 업데이트
        UpdateWire();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        // 드래그 종료 시 목표 지점과의 거리 확인
        float distance = Vector2.Distance(endPoint.anchoredPosition, targetPoint.anchoredPosition);
        if (distance < 50f) // 연결 성공 (거리 기준)
        {
            isConnected = true;
            IsCorrectConnection = true; // 올바른 연결로 설정
            endPoint.anchoredPosition = targetPoint.anchoredPosition;
            UpdateWire();
            Debug.Log("전선 연결 성공!");
        }
        else // 연결 실패
        {
            ResetWire();
            Debug.Log("전선 연결 실패!");
        }
    }

    private void UpdateWire()
    {
        // 전선 이미지의 위치와 크기 업데이트
        Vector2 direction = endPoint.anchoredPosition - startPoint.anchoredPosition;
        float distance = direction.magnitude;
        wireImage.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, wireImage.GetComponent<RectTransform>().sizeDelta.y);
        wireImage.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        wireImage.GetComponent<RectTransform>().anchoredPosition = startPoint.anchoredPosition;
        wireImage.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void ResetWire()
    {
        // 전선 초기화
        endPoint.anchoredPosition = startPoint.anchoredPosition;
        UpdateWire();
        wireImage.enabled = false;
    }
}