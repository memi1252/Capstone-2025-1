using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float jumpDistance = 1.1f;
    [SerializeField] public float thrustPower = 5f;  // 이동 속도 (추진력)
    [SerializeField] public float rotationSpeed = 2f; // 회전 속도
    [SerializeField] private float rollSpeed = 50f; // Q, E 키로 회전하는 속도
    [SerializeField] private float boostMultiplier = 2f; // 부스터 속도 배율
    [SerializeField] private float ItemPickUpDistance; // 아이템 획득 거리
    [SerializeField] private GameObject Rope;
    [SerializeField] public bool isMove = true;
    private bool isjump = false;
    public List<string> haveKeycode = new List<string>();

    [Header("직압대 변수")]
    public Vector3 CameracraftingPositon;
    public Quaternion CameracraftingRotation;
    private Vector3 CameraOriginalPosition;
    private Quaternion CameraOriginalRotation;
    private Transform CraftingTableTransform;
    
    private Rigidbody rigidbody;
    public Animator animator;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.linearDamping = 0;
        rigidbody.angularDamping = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    


    private void Update()
    {
        if (GameManager.Instance.ismove)
        {
            if(Rope != null)
                Rope.transform.position = transform.position;
            
            HandleMovement();
            //HandleRotation();
            //HandleRoll();
            
        }
        PickUpItem();
        CameracraftingMove();
    }


    private bool isRun;
    void HandleMovement()
    {
        if (isMove)
        {
            if (GameManager.Instance.inSpaceShip)
            {
                
                        Vector3 moveDirection = Vector3.zero;

                        // 기본 이동
                        if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward; // 전진
                        if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward; // 후진
                        if (Input.GetKey(KeyCode.A)) moveDirection -= transform.right;   // 좌측 이동
                        if (Input.GetKey(KeyCode.D)) moveDirection += transform.right;   // 우측 이동

                        if (moveDirection != Vector3.zero)
                        {
                            animator.SetBool("walk", true);
                            animator.SetBool("Run", isRun);
                        }
                        else
                        {
                            animator.SetBool("walk", false);
                            animator.SetBool("Run", false);
                        }
                        // 달리기
                        float currentSpeed = moveSpeed;
                        if (Input.GetKey(KeyCode.LeftShift)) // 달리기
                        {
                            isRun = true;
                        }
                        else
                        {
                            isRun = false;
                        }
                        
                        if (!isRun)
                        {
                            currentSpeed = moveSpeed/2 ; // 달리기 속도
                        }
                        else
                        {
                            currentSpeed = moveSpeed;
                        }

                        // 이동 처리
                        transform.position += moveDirection * currentSpeed * Time.deltaTime;

                        // 점프 처리
                        if (Input.GetKeyDown(KeyCode.Space) && !isjump)
                        {
                            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                            isjump = true;
                        }
            }
            else
            {
                Vector3 moveDirection = Vector3.zero;

                if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward * Time.deltaTime * moveSpeed; // 전진
                if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward * Time.deltaTime * moveSpeed; // 후진
                if (Input.GetKey(KeyCode.A)) moveDirection -= transform.right * Time.deltaTime * moveSpeed; // 좌측 이동
                if (Input.GetKey(KeyCode.D)) moveDirection += transform.right * Time.deltaTime * moveSpeed; // 우측 이동
                if (Input.GetKey(KeyCode.Space)) moveDirection += transform.up * Time.deltaTime * moveSpeed; // 상승
                if (Input.GetKey(KeyCode.LeftControl)) moveDirection -= transform.up * Time.deltaTime * moveSpeed; // 하강

                float speed = thrustPower;

                if (Input.GetKey(KeyCode.LeftShift)) // 부스터 기능
                {
                    speed *= boostMultiplier;
                }

                rigidbody.AddForce(moveDirection * speed, ForceMode.Acceleration);
            }
        }
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.up * mouseX, Space.Self);
        transform.Rotate(Vector3.left * mouseY, Space.Self);
    }
    
    

    
    private item lookAtItem;
    void PickUpItem()
    {
        if(Camera.main == null) return;
        RaycastHit hit;
        Vector3 origin = Camera.main.transform.position;
        Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * ItemPickUpDistance, Color.red);
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit, ItemPickUpDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                item item = hit.transform.GetComponent<item>();
                lookAtItem = item;
                if (!item.outline)
                {
                    item.outline = true;
                    item.GetComponentInChildren<Renderer>().materials[1].SetFloat("_outlien_thickness", 0.01f);
                }
                if (Input.GetKeyDown(KeyCode.F) && !item.isFrontItem)
                {
                    item.frontitem(hit.collider.gameObject);
                    item.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Item");
                }
                if (Input.GetKeyDown(KeyCode.F) && item.isRotateItem)
                {
                    item.Pickup();
                }
                UIManager.Instance.tooltipUI.SetText(item.itemName);
            }else if (hit.collider.CompareTag("cockpit"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GameManager.Instance.ismove = false;
                    GameManager.Instance.isCamera = false;
                    GameManager.Instance.MouseCursor(true);
                    SPACESTART spaceStart = FindAnyObjectByType<SPACESTART>();
                    if (spaceStart != null)
                    {
                        spaceStart.transform.parent.parent.gameObject.SetActive(false);
                        spaceStart.doking.SetActive(true);
                        UIManager.Instance.tooltipUI.Hide();
                        rigidbody.linearVelocity = Vector3.zero;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.LogError("SPACESTART not found in the scene.");
                    }
                }
                UIManager.Instance.tooltipUI.SetText("F를 눌러 조종시작");
            }
            else
            {
                if (lookAtItem != null)
                {
                    lookAtItem.outline = false;
                    lookAtItem.GetComponentInChildren<Renderer>().materials[1].SetFloat("_outlien_thickness", 0.0f);
                }
                lookAtItem = null;
                UIManager.Instance.tooltipUI.Hide();
            }
            
            
            
        }else
        {
            
            if (lookAtItem != null)
            {
                if(lookAtItem.tag != "item") return;
                lookAtItem.outline = false;
                lookAtItem.GetComponentInChildren<Renderer>().materials[1].SetFloat("_outlien_thickness", 0.0f);
            }
            lookAtItem = null;
            UIManager.Instance.tooltipUI.Hide();
        }
        
        
        RaycastHit hit2;
        Vector3 origin2 = Camera.main.transform.position;
        Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * (ItemPickUpDistance-4), Color.red);
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit, ItemPickUpDistance - 4f))
        {
            if (hit.collider.CompareTag("crafting table"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (UIManager.Instance.ProductUI.activeSelf)
                    {
                        GameManager.Instance.MouseCursor(false);
                        UIManager.Instance.ProductUI.SetActive(false);
                        UIManager.Instance.ProductSlotUI.SetActive(false);
                        UIManager.Instance.InvneoryUI.SetActive(false);
                        UIManager.Instance.QuitSlotUI.SetActive(false);
                        backToCrafting = true;
                    }
                    else
                    {
                        CraftingTableTransform = hit.collider.transform;
                        CameraOriginalPosition = Camera.main.transform.position;
                        CameraOriginalRotation = Camera.main.transform.rotation;
                        GameManager.Instance.ismove = false;
                        GameManager.Instance.isCamera = false;
                        isCrafting = true;
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                }
                UIManager.Instance.tooltipUI.SetText("F를 눌러 작업대 열기");
                
            }
        }
    }
    
    bool isCrafting = false;
    bool goToCrafting = false;
    bool backToCrafting = false;
    private void CameracraftingMove()
    {
        if (isCrafting)
        {
            UIManager.Instance.tooltipUI.gameObject.SetActive(false);
            if (!goToCrafting)
            {
                if (Camera.main == null) return;
                Camera.main.transform.SetParent(CraftingTableTransform);
                Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, CameracraftingPositon, 3 * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, CameracraftingRotation, 150 * Time.deltaTime);
                if (Camera.main.transform.localPosition == CameracraftingPositon &&
                    Camera.main.transform.rotation == CameracraftingRotation)
                {
                    UIManager.Instance.ProductUI.SetActive(true);
                    UIManager.Instance.ProductSlotUI.SetActive(true);
                    UIManager.Instance.InvneoryUI.SetActive(true);
                    UIManager.Instance.QuitSlotUI.SetActive(true);
                    GameManager.Instance.MouseCursor(true);
                    goToCrafting = true;
                }
                    
            }

            if (backToCrafting)
            {
                if (Camera.main == null) return;
                Camera.main.transform.SetParent(transform);
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CameraOriginalPosition, 3 * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, CameraOriginalRotation, 150 * Time.deltaTime);
                if (Camera.main.transform.position == CameraOriginalPosition &&
                    Camera.main.transform.rotation == CameraOriginalRotation)
                {
                    backToCrafting = false;
                    goToCrafting = false;
                    isCrafting = false;
                    GameManager.Instance.ismove = true;
                    GameManager.Instance.isCamera = true;
                }
            }
            
        }
       
    }
}