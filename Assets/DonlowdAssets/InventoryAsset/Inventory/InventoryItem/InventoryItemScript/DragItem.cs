using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    // 작성자: Jaxon Schauer (수정됨)
    /// <summary>
    /// 아이템 드래그를 제어합니다.
    /// </summary>
    internal class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        /// 드래그 아이템과 연결된 현재 슬롯
        private Slot CurrentSlot;

        /// 드래그 중인 인벤토리 아이템
        private InventoryItem item;

        /// 아이템 정보를 표시하는 텍스트 UI 요소
        [SerializeField, HideInInspector]
        private TextMeshProUGUI text;

        // 아이템이 놓치면 어떻게 동작할지 결정
        private bool returnOnMiss = false;
        private bool dropped = true;
        private bool isHeld = false; // 아이템이 들고 있는 상태인지 확인
        
        /// 아이템 정보를 표시하는 텍스트 UI 요소
        private GameObject prevslot;
        
        // 아이템 분리 관련 변수
        private bool isDragging = false;
        private Vector2 dragStartPosition;
        private float splitThreshold = 30f; // 아이템 분리를 위한 드래그 거리 임계값

        /// Start에서 CurrentSlot을 초기화합니다.
        private void Start()
        {
            prevslot = null;
            CurrentSlot = transform.parent.GetComponent<Slot>();
        }

        /// <summary>
        /// 초기화 메서드
        /// </summary>
        public void Initiailize()
        {
            if (transform.GetChild(0) != null)
            {
                if (transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    text = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.LogError("Slot 자식 객체에 TextMeshPro가 없습니다.");
                }
            }
            else
            {
                Debug.LogError("Slot 자식 객체가 null입니다.");
            }
        }

        /// <summary>
        /// 마우스 클릭 이벤트를 처리합니다.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            // 오른쪽 클릭이면 무시
            if (eventData.button == PointerEventData.InputButton.Right)
                return;
                
            if (Draggable()) return;
            
            // 클릭하면 아이템을 들고 있는 상태로 변경
            if (!isHeld && !isDragging)
            {
                isHeld = true;
                CurrentSlot.ResetSlot();
                transform.SetParent(CurrentSlot.GetInventoryUI().GetUI());
                
                // 아이템을 마우스 위치로 이동
                FollowMouse(eventData);
            }
            else if (isHeld)
            {
                // 들고 있는 상태에서 클릭하면 아이템 배치 시도
                TryPlaceItem(eventData);
            }
        }

        /// <summary>
        /// 마우스를 따라 아이템을 이동시킵니다.
        /// </summary>
        private void FollowMouse(PointerEventData eventData)
        {
            // 캔버스와 RectTransform 가져오기
            Canvas canvas = InventoryController.instance.GetUI().GetComponent<Canvas>();
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            // 화면 좌표를 캔버스 상대 좌표로 변환
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                // 아이템 위치를 캔버스 상대 좌표로 업데이트
                transform.localPosition = localPointerPosition;
            }

            transform.parent.gameObject.transform.SetAsLastSibling(); // 드래그된 아이템이 항상 위에 표시되도록 설정
        }

        /// <summary>
        /// 드래그 이벤트를 처리합니다.
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            if (Draggable()) return;

            isDragging = true;
            
            // 아이템이 들고 있는 상태가 아니면 들고 있는 상태로 변경
            if (!isHeld && dropped)
            {
                isHeld = true;
                dropped = false;
                CurrentSlot.ResetSlot();
                transform.SetParent(CurrentSlot.GetInventoryUI().GetUI());
                
                // 드래그 시작 위치 저장
                dragStartPosition = eventData.position;
            }

            // 마우스 따라가기
            FollowMouse(eventData);

            // 하이라이트 처리
            UpdateHighlight(eventData);
            
            // 드래그 거리를 확인하고 아이템 분리 처리
            CheckForSplitItem(eventData);
        }
        
        /// <summary>
        /// 아이템 분리 가능 여부를 확인하고 처리합니다.
        /// </summary>
        private void CheckForSplitItem(PointerEventData eventData)
        {
            
            float dragDistance = Vector2.Distance(dragStartPosition, eventData.position);
            
            if (dragDistance > splitThreshold && Input.GetMouseButton(0) && item.GetAmount() > 1)
            {
                Slot targetSlot = FindEmptySlotUnderPointer(eventData);
                
                if (targetSlot != null && targetSlot.GetItem().GetIsNull() && 
                    targetSlot.GetInventoryUI().GetInventory().CheckAcceptance(item.GetItemType()))
                {
                    SplitItem(targetSlot);
                    
                    dragStartPosition = eventData.position;
                }
            }
        }
        
        /// <summary>
        /// 포인터 위치에서 빈 슬롯을 찾습니다.
        /// </summary>
        private Slot FindEmptySlotUnderPointer(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Slot"))
                {
                    Slot slot = result.gameObject.GetComponent<Slot>();
                    if (slot.GetItem().GetIsNull())
                    {
                        return slot;
                    }
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// 아이템을 1개 분리하여 대상 슬롯에 배치합니다.
        /// </summary>
        private void SplitItem(Slot targetSlot)
        {
            InventoryItem splitItem = new InventoryItem(item);
            splitItem.SetAmount(1);
            
            item.SetAmount(item.GetAmount() - 1);
            
            SetText();
            
            InventoryController.instance.AddItemPos(targetSlot.GetInventoryUI().GetInventoryName(), splitItem, targetSlot.GetPosition());
        }
        
        /// <summary>
        /// 하이라이트 업데이트 처리
        /// </summary>
        private void UpdateHighlight(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            bool foundSlot = false;

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Slot"))
                {
                    Slot slot = result.gameObject.GetComponent<Slot>();
                    if (slot.GetItem().GetIsNull() && slot.GetInventoryUI().GetInventory().CheckAcceptance(item.GetItemType()))
                    {
                        slot.GetInventoryUI().Highlight(result.gameObject);
                        foundSlot = true;
                    }
                    if (prevslot != null && prevslot != result.gameObject)
                    {
                        prevslot.GetComponent<Slot>().GetInventoryUI().UnHighlight(prevslot);
                        prevslot.GetComponent<Slot>().GetInventoryUI().ResetHighlight();
                    }
                    prevslot = result.gameObject;
                    break;
                }
            }

            if (!foundSlot)
            {
                if (prevslot != null)
                {
                    prevslot.GetComponent<Slot>().GetInventoryUI().UnHighlight(prevslot);
                    prevslot.GetComponent<Slot>().GetInventoryUI().ResetHighlight();
                }
            }
        }

        /// <summary>
        /// 드래그 시작 이벤트를 처리합니다.
        /// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Draggable()) return;
            
            isDragging = true;
            
            if (CurrentSlot != null && dropped && !isHeld)
            {
                isHeld = true;
                dropped = false;
                CurrentSlot.ResetSlot();
                transform.SetParent(CurrentSlot.GetInventoryUI().GetUI());
                
                dragStartPosition = eventData.position;
            }
            else if (!isHeld)
            {
                Debug.LogWarning("슬롯이 없습니다.");
            }
        }

        /// <summary>
        /// 드래그 종료 이벤트를 처리합니다.
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (Draggable()) return;
            
            isDragging = false;
            
            // 아이템을 들고 있는 상태라면 계속 마우스를 따라다니게 함
            if (isHeld)
            {
                FollowMouse(eventData);
            }
            else
            {
                // 아이템을 들고 있지 않다면 기존 로직 실행
                HandleEndDrag(eventData);
            }
        }
        
        /// <summary>
        /// 들고 있는 아이템을 배치하려고 시도합니다.
        /// </summary>
        private void TryPlaceItem(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            bool foundSlot = false;

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Slot"))
                {
                    HandleSlot(result);
                    foundSlot = true;
                    break;
                }
            }

            if (!foundSlot)
            {
                // 슬롯이 아닌 곳에 클릭하면 아이템 유지
                // 여기서는 추가 처리 없이 계속 들고 있게 함
            }
            
            // 하이라이트 상태 초기화
            if (prevslot != null)
            {
                prevslot.GetComponent<Slot>().GetInventoryUI().UnHighlight(prevslot);
                prevslot.GetComponent<Slot>().GetInventoryUI().ResetHighlight();
            }
        }

        /// <summary>
        /// 드래그 종료 후 유효한 드롭 대상 확인을 처리합니다.
        /// </summary>
        private void HandleEndDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            bool foundSlot = false;

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Slot"))
                {
                    HandleSlot(result);
                    foundSlot = true;
                    break;
                }
            }

            if (!foundSlot)
            {
                HandleInvalidPlacement();
            }
        }

        /// <summary>
        /// 드래그 후 슬롯 결과를 처리합니다.
        /// </summary>
        private void HandleSlot(RaycastResult result)
        {
            Slot slot = result.gameObject.GetComponent<Slot>();
            bool slotNull = slot.GetItem().GetIsNull();
            bool itemStackable = !slot.GetItem().GetIsNull() && (slot.GetItem().GetItemType() == item.GetItemType()) && (slot.GetItem().GetAmount() + item.GetAmount()) <= slot.GetItem().GetItemStackAmount();
            bool itemAcceptedInInventory = slot.GetInventoryUI().GetInventory().CheckAcceptance(item.GetItemType());

            if ((slotNull || itemStackable) && itemAcceptedInInventory)
            {
                InventoryController.instance.AddItemPos(slot.GetInventoryUI().GetInventoryName(), item, slot.GetPosition());
                slot.GetInventoryUI().UnHighlight(result.gameObject);
                
                if (prevslot != null)
                {
                    prevslot.GetComponent<Slot>().GetInventoryUI().ResetHighlight();
                }
                
                isHeld = false;
                dropped = true;
                Destroy(gameObject);
            }
            else
            {
                if (itemAcceptedInInventory)
                {
                    HandleInvalidPlacementOverInv(slot.GetItem());
                }
                else
                {
                    // 인벤토리에 허용되지 않는 아이템이면 계속 들고 있음
                    isHeld = true;
                }
            }
        }

        /// <summary>
        /// 사용자 입력에 따라 유효하지 않은 배치를 처리합니다. 아이템을 삭제하거나 원래 위치로 반환하거나 사용자 지정 함수를 호출할 수 있습니다.
        /// </summary>
        private void HandleInvalidPlacement(bool isOverride = false)
        {
            // 들고 있는 상태라면 계속 들고 있음
            if (isHeld && !isOverride)
            {
                return;
            }
            
            Vector3 mousePosition = Input.mousePosition;
            Camera cam = Camera.main;
            Vector3 worldPosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));
            
            if (returnOnMiss || isOverride)
            {
                InventoryController.instance.AddItemPos(CurrentSlot.GetInventoryUI().GetInventoryName(), item, CurrentSlot.GetPosition());
                
                if (CurrentSlot != null)
                {
                    CurrentSlot.GetInventoryUI().UnHighlight(CurrentSlot.gameObject);
                }
                
                if (!isOverride)
                {
                    CurrentSlot.GetInventoryUI().InvokeMiss(worldPosition, item);
                }
                
                isHeld = false;
                dropped = true;
                Destroy(gameObject);
            }
            else
            {
                CurrentSlot.GetInventoryUI().InvokeMiss(worldPosition, item);
                isHeld = false;
                dropped = true;
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 슬롯 위에서의 유효하지 않은 배치를 처리합니다.
        /// </summary>
        private void HandleInvalidPlacementOverInv(InventoryItem inSlot)
        {
            if (CurrentSlot.GetInventoryUI().InvokeMissOverSlot(item, inSlot))
            {
                isHeld = false;
                Destroy(gameObject);
            }
            else
            {
                HandleInvalidPlacement(true);
            }
        }

        /// <summary>
        /// 아이템이 드래그 가능한지 확인합니다.
        /// </summary>
        private bool Draggable()
        {
            return CurrentSlot != null && (!item.GetDraggable() || !CurrentSlot.GetInventoryUI().GetDraggable());
        }

        /// <summary>
        /// 드래그 아이템에 인벤토리 아이템을 설정합니다.
        /// </summary>
        public void SetItem(InventoryItem newItem)
        {
            item = newItem;
        }

        /// <summary>
        /// 아이템 속성에 따라 텍스트 UI를 업데이트합니다.
        /// </summary>
        public void SetText()
        {
            if (!item.GetIsNull())
            {
                text.gameObject.SetActive(item.GetDisplayAmount());
                if (item.GetDisplayAmount())
                {
                    text.SetText(item.GetAmount().ToString());
                }
            }
        }

        /// <summary>
        /// 테스트용 텍스트를 설정합니다.
        /// </summary>
        public void SetTextTestImage(int amount)
        {
            text.SetText(amount.ToString());
        }

        /// <summary>
        /// 텍스트 위치의 오프셋을 설정합니다.
        /// </summary>
        public void SetTextPositionOffset(Vector3 offset)
        {
            text.gameObject.transform.position += offset;
        }

        /// <summary>
        /// 텍스트 UI의 폰트 크기를 설정합니다.
        /// </summary>
        public void SetTextSize(float size)
        {
            text.fontSize = size;
        }

        public void SetImageSize(Vector2 size)
        {
            RectTransform imageRect = GetComponent<RectTransform>();
            imageRect.sizeDelta = size;
        }

        public void SetImage(Sprite image)
        {
            GetComponent<Image>().sprite = image;
        }

        public float GetTextSize()
        {
            return text.fontSize;
        }

        public Vector2 GetTextPosition()
        {
            return text.transform.localPosition;
        }

        public void SetTextPosition(Vector2 textposition)
        {
            text.transform.localPosition = textposition;
        }

        public void SetImagePositionOffset(Vector3 imagePostionOffset)
        {
            transform.position += imagePostionOffset;
        }

        public void SetReturnOnMiss(bool returnOnMiss)
        {
            this.returnOnMiss = returnOnMiss;
        }
        
        /// <summary>
        /// 매 프레임마다 마우스를 따라가게 합니다.
        /// </summary>
        private void Update()
        {
            // 아이템을 들고 있는 상태이고 드래그 중이 아니면 마우스를 따라가게 함
            if (isHeld && !isDragging)
            {
                Vector2 mousePosition = Input.mousePosition;
                
                // 캔버스와 RectTransform 가져오기
                Canvas canvas = InventoryController.instance.GetUI().GetComponent<Canvas>();
                RectTransform canvasRect = canvas.GetComponent<RectTransform>();

                // 화면 좌표를 캔버스 상대 좌표로 변환
                Vector2 localPointerPosition;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePosition, null, out localPointerPosition))
                {
                    // 아이템 위치를 캔버스 상대 좌표로 업데이트
                    transform.localPosition = localPointerPosition;
                }
            }
        }
    }
}