using System;
using System.Collections.Generic;
using Doublsb.Dialog;
using InventorySystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


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
    [SerializeField] private float ItemDetectionRadius = 15; //아이템 감지 반경
    [SerializeField] private GameObject Rope;
    [SerializeField] public bool isMove = true;
    [SerializeField] private GameObject ItemPickupHelpUI;
    public AudioSource ItemPickupSound;
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

    private void Start()
    {
        CameraOriginalPosition = Camera.main.transform.localPosition;
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

    private float h;
    private float v;
    private float currentSpeed;
    void HandleMovement()
    {
        if (isMove)
        {
            if (GameManager.Instance.inSpaceShip)
            {
                animator.SetBool("SpaceShipIn", true);
                Camera.main.transform.localPosition = CameraOriginalPosition;

                
                if (!isjump)
                {
                    h = Input.GetAxisRaw("Horizontal");
                    v = Input.GetAxisRaw("Vertical");
                }
                
                
                
                //if(Mathf.Abs(h) < 0.4f && Mathf.Abs(v) < 0.4f) rigidbody.linearVelocity = Vector3.zero;
                
                Vector3 cameraForward = Camera.main.transform.forward;
                Vector3 cameraRight = Camera.main.transform.right;
                
                cameraForward.y = 0;
                cameraRight.y = 0;
                cameraForward.Normalize();
                cameraRight.Normalize();

                Vector3 moveDirection = (cameraForward * v + cameraRight * h).normalized;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isRun = true;
                }
                else
                {
                    isRun = false;
                }
                
                currentSpeed = isRun ? moveSpeed +2 : moveSpeed;
                Vector3 velocity = moveDirection * currentSpeed;

                if (moveDirection != Vector3.zero)
                {
                    rigidbody.linearVelocity = new Vector3(velocity.x, rigidbody.linearVelocity.y, velocity.z);
                    animator.SetBool("walk", true);
                    animator.SetBool("Run", isRun); // 달리기 애니메이션 제거
                }
                else
                {
                    rigidbody.linearVelocity = new Vector3(0, rigidbody.linearVelocity.y, 0);
                    animator.SetBool("walk", false);
                    animator.SetBool("Run", false);
                }

                if (Input.GetKeyDown(KeyCode.Space) && !isjump) // 점프
                {
                    isjump = true;
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
            else
            { 
                animator.SetBool("SpaceShipIn", false);
                
                Vector3 moveDirection = Vector3.zero;
                
                if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward * Time.deltaTime * moveSpeed; //전진
                if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward * Time.deltaTime * moveSpeed; // 후진
                if (Input.GetKey(KeyCode.A)) moveDirection -= transform.right * Time.deltaTime * moveSpeed; // 좌측 이동
                if (Input.GetKey(KeyCode.D)) moveDirection += transform.right * Time.deltaTime * moveSpeed; // 우측 이동
                if (Input.GetKey(KeyCode.Space)) moveDirection += transform.up * Time.deltaTime * moveSpeed; // 상승
                if (Input.GetKey(KeyCode.LeftControl)) moveDirection -= transform.up * Time.deltaTime * moveSpeed; // 하강

                float speed = thrustPower;

                if (Input.GetKey(KeyCode.LeftShift)) // 부스터 기능
                {
                    speed *= boostMultiplier;
                    isRun = true;
                }
                else
                {
                    isRun = false;
                }

                rigidbody.AddForce(moveDirection * speed, ForceMode.Acceleration);
            }
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("Run", false);
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isjump = false; // 땅에 닿으면 점프 가능
        }
    }
    
    
private bool keycodeCheck = false;
    
    void PickUpItem()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, ItemDetectionRadius); 
        
        
        var allItems = FindObjectsOfType<item>();
        if (allItems != null)
        {
            foreach (var item in allItems)
            {
                if (item.GetComponent<Outline>() != null)
                {
                    Outline outline = item.GetComponent<Outline>();
                    outline.enabled = false;
                }
                // if (item != null && item.GetComponentInChildren<Renderer>() != null)
                // {
                //     var renderer = item.GetComponentInChildren<Renderer>();
                //     foreach (var mat in renderer.materials)
                //     {
                //         if (mat.HasProperty("_outlien_thickness"))
                //         {
                //             item.outline = false;
                //             mat.SetFloat("_outlien_thickness", 0.0f);
                //         }
                //     }
                // }
            }
        }
        
        // 콜라이더 안에 있는 아이템만 아웃라인 활성화
        foreach (var collider in items)
        {
            var item = collider.GetComponent<item>();
            if (item != null && item.GetComponentInChildren<Renderer>() != null)
            {
                if (collider.GetComponent<Outline>() != null)
                {
                    Outline outline = collider.GetComponent<Outline>();
                    outline.enabled = true;
                }
                
            }
            
        }
        
        bool itemlook = false;
        
        if(Camera.main == null) return;
        RaycastHit hit;
        Vector3 origin = Camera.main.transform.position;
        Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * ItemPickUpDistance, Color.red);
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit, ItemPickUpDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                itemlook = true;
                item item = hit.transform.GetComponent<item>();
                
                if (Input.GetKeyDown(KeyCode.F) && !item.isFrontItem)
                {
                    item.frontitem(hit.collider.gameObject);
                    if(ItemPickupHelpUI.activeSelf)
                        ItemPickupHelpUI.SetActive(false);
                    rigidbody.linearVelocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                    item.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Item");
                }
                if (Input.GetKeyDown(KeyCode.F) && item.isRotateItem)
                {
                    item.Pickup();
                }
                UIManager.Instance.tooltipUI.SetText(item.itemName);
            }
            else
            {
                UIManager.Instance.tooltipUI.Hide();
            }
        }
        
        
        RaycastHit hit2;
        if (Camera.main != null)
        {
            Vector3 origin2 = Camera.main.transform.position;
            Debug.DrawRay(origin2, Camera.main.transform.forward * (ItemPickUpDistance-3f), Color.red);
            if (Physics.Raycast(origin2, Camera.main.transform.forward, out hit2, ItemPickUpDistance - 3f) &&
                !GameManager.Instance.isInventoryOpen && !UIManager.Instance.ESCMENUUI.activeSelf)
            {
                if (hit2.collider.CompareTag("crafting table"))
                {
                    if (Input.GetKeyDown(KeyCode.F)  )
                    {
                        if (UIManager.Instance.ProductUI.activeSelf)
                        {
                            GameManager.Instance.MouseCursor(false);
                            UIManager.Instance.ProductUI.SetActive(false);
                            UIManager.Instance.ProductSlotUI.SetActive(false);
                            UIManager.Instance.InvneoryUI.SetActive(false);
                            UIManager.Instance.QuitSlotUI.SetActive(false);
                            UIManager.Instance.StastUI.SetActive(true);
                            backToCrafting = true;
                            GameManager.Instance.noInventoryOpen = false;
                        }
                        else if(!isCrafting)
                        {
                            GameManager.Instance.noESC = true;
                            CraftingTableTransform = hit.collider.transform;
                            CameraOriginalPosition = Camera.main.transform.localPosition;
                            CameraOriginalRotation = Camera.main.transform.rotation;
                            UIManager.Instance.StastUI.SetActive(false);
                            GameManager.Instance.ismove = false;
                            GameManager.Instance.isCamera = false;
                            isCrafting = true;
                            rigidbody.linearVelocity = Vector3.zero;
                            GameManager.Instance.noInventoryOpen = true;
                        }
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                    UIManager.Instance.tooltipUI.SetText("F를 눌러 작업대 열기");
                }
                else if (hit2.collider.CompareTag("cockpit"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        GameManager.Instance.ismove = false;
                        GameManager.Instance.MouseCursor(true);
                        UIManager.Instance.StastUI.SetActive(false);
                        UIManager.Instance.QuitSlotUI.SetActive(false);
                        SPACESTART spaceStart = FindAnyObjectByType<SPACESTART>();
                        if (spaceStart != null)
                        {
                            spaceStart.transform.parent.gameObject.SetActive(false);
                            spaceStart.doking.SetActive(true);
                            spaceStart.doking.GetComponent<dockingSysyem>().ss = spaceStart;
                            UIManager.Instance.tooltipUI.Hide();
                            rigidbody.linearVelocity = Vector3.zero;
                            gameObject.SetActive(false);
                            GameManager.Instance.noInventoryOpen = true;
                            UIManager.Instance.tutorialsUI.gameObject.SetActive(false);
                            QuestManager.Instance.quests[0].clear = true;
                        }
                        GameManager.Instance.isdoking = true;
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                    UIManager.Instance.tooltipUI.SetText("F를 눌러 조종시작");
                }else if (hit2.collider.CompareTag("BBAAbattery"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hit2.collider.GetComponent<BBAAbattery>().charing = true;
                        FindAnyObjectByType<SPACESTART>().ispos1 = false;
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                    UIManager.Instance.tooltipUI.SetText("F를 눌러 BBASS 배터리 충전");
                }else if (hit2.collider.CompareTag("BBASS"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        rigidbody.linearVelocity = Vector3.zero;
                        if (hit2.collider.GetComponent<BBASS_Ment1>().enabled &&
                            FindAnyObjectByType<SPACESTART>().BBASSMove == false)
                        {
                            if (!hit2.collider.GetComponent<BBASS_Ment1>().play)
                            {
                                hit2.collider.GetComponent<BBASS_Ment1>().line();
                                GameManager.Instance.ismove = false;
                                GameManager.Instance.isCamera = false;
                                GameManager.Instance.MouseCursor(true);
                                UIManager.Instance.StastUI.SetActive(false);
                                UIManager.Instance.QuitSlotUI.SetActive(false);
                            }
                        }else if (hit2.collider.GetComponent<BBASS_Ment2>().enabled)
                        {
                            if (!hit2.collider.GetComponent<BBASS_Ment2>().play)
                            {
                                hit2.collider.GetComponent<BBASS_Ment2>().line();
                                GameManager.Instance.ismove = false;
                                GameManager.Instance.isCamera = false;
                                GameManager.Instance.MouseCursor(true);
                                UIManager.Instance.StastUI.SetActive(false);
                                UIManager.Instance.QuitSlotUI.SetActive(false);
                            }
                        }else if (hit2.collider.GetComponent<BBASS_Ment3>().enabled)
                        {
                            if (!hit2.collider.GetComponent<BBASS_Ment3>().play)
                                hit2.collider.GetComponent<BBASS_Ment3>().line();
                        }else if (hit2.collider.GetComponent<BBASS_Ment4>().enabled)
                        {
                            if (!hit2.collider.GetComponent<BBASS_Ment4>().play)
                            {
                                hit2.collider.GetComponent<BBASS_Ment4>().line();
                            }
                        }
                        else if (hit2.collider.GetComponent<BBASS_Ment5>().enabled)
                        {
                            if (!hit2.collider.GetComponent<BBASS_Ment5>().play)
                            {
                                hit2.collider.GetComponent<BBASS_Ment5>().line();
                            }
                        }
                        Debug.Log(hit2.collider.name);
                    }
                    UIManager.Instance.tooltipUI.SetText("F를 눌러 BBASS와 대화");
                }else if (hit2.collider.GetComponent<CabinetDoor>())
                {
                    CabinetDoor cabinetDoor = hit2.collider.GetComponent<CabinetDoor>();
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (cabinetDoor.isOpen)
                        {
                            cabinetDoor.Close();
                        }
                        else
                        {
                            cabinetDoor.Open();
                        }
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                }else if (hit2.collider.CompareTag("WireConnectionDoor"))
                {
                    if (Input.GetKeyDown(KeyCode.F) && !hit.collider.GetComponent<WireConnectionDoor>().Clear)
                    {
                        hit2.collider.GetComponent<WireConnectionDoor>().Open();
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                    if(!hit.collider.GetComponent<WireConnectionDoor>().Clear)
                        UIManager.Instance.tooltipUI.SetText("F를 눌러 전력 분배기문 열기(니퍼 필요)");
                }else if (hit2.collider.CompareTag("ReplacingpartsDoor"))
                {
                    if (Input.GetKeyDown(KeyCode.F) && !hit.collider.GetComponent<ReplacingpartsDoor>().Clear)
                    {
                        hit2.collider.GetComponent<ReplacingpartsDoor>().Open();
                        rigidbody.linearVelocity = Vector3.zero;
                    }
                        
                    if (!hit.collider.GetComponent<ReplacingpartsDoor>().Clear)
                        UIManager.Instance.tooltipUI.SetText("F를 눌러 추진 제어판문 열기(몽키스패너 필요)");
                }else if (hit2.collider.CompareTag("Fliter"))
                {
                    FliterSystem fs = FindAnyObjectByType<FliterSystem>();
                    if (fs.isbroken && fs.off && !fs.outFliter)
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            hit2.collider.transform.GetChild(1).gameObject.SetActive(false);
                            fs.outFliter = true;
                        }
                        UIManager.Instance.tooltipUI.SetText("F를 눌러 필터 뺴기");
                    }
                    else if (fs.isbroken && !fs.off && !fs.outFliter)
                    {
                        UIManager.Instance.tooltipUI.SetText("생명 유지장치를 꺼주세요");
                    }else if (fs.isbroken && fs.off && fs.outFliter)
                    {
                        UIManager.Instance.tooltipUI.SetText("F를 눌러 필터 넣기(필터를 들고있어야함)");
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            QuitslotItemSelect qsi = GameManager.Instance.player.GetComponent<QuitslotItemSelect>();
                            if (qsi.currentHandItem.name ==
                                "fliterItem")
                            {
                                var item = InventoryController.instance.GetItem(qsi.inventoryName,qsi.currentHandItemIndex);
                                InventoryController.instance.RemoveItem(qsi.inventoryName,item, 1);
                                hit2.collider.transform.GetChild(1).gameObject.SetActive(true);
                                fs.isbroken = false;
                                fs.clear = true;
                            }
                        }
                    }
                }else if (hit2.collider.CompareTag("FliterOnOff"))
                {
                    FliterSystem fs = FindAnyObjectByType<FliterSystem>();
                    if (fs.isbroken)
                    {
                        fs.off = !fs.off;
                        if (fs.off)
                        {
                            UIManager.Instance.tooltipUI.SetText("생명 유지장치 켜기");
                        }
                        else
                        {
                            UIManager.Instance.tooltipUI.SetText("생명 유지장치 끄기");
                        }
                    }
                }
                else if (hit2.collider.CompareTag("StationDoor"))
                {
                    keycodeCheck = false;
                    foreach (var key in haveKeycode)
                    {
                        if (hit2.collider.GetComponentInChildren<ActiveColGo>().keycode == key)
                        {
                            keycodeCheck = true;
                        }
                    }

                    if (!keycodeCheck)
                    {
                        UIManager.Instance.tooltipUI.SetText("카드키가 필요합니다.");
                    }                    
                }else if (hit2.collider.CompareTag("BED"))
                {
                    BED bed = hit2.collider.GetComponent<BED>();
                    if (bed.goodNight)
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            bed.GoToSleep();
                            rigidbody.linearVelocity = Vector3.zero;
                        }
                        UIManager.Instance.tooltipUI.SetText("F를 눌러 잠자기");
                    }
                    else
                    {
                        UIManager.Instance.tooltipUI.SetText("아직 잠을 잘수 없습니다.");
                    }
                }else if (hit2.collider.CompareTag("Door"))
                {
                    if (hit2.collider.GetComponentInChildren<ActiveColGo>().keyActive)
                    {
                        if (!hit2.collider.GetComponentInChildren<ActiveColGo>().hasValidKey)
                        {
                            UIManager.Instance.tooltipUI.SetText("카드키가 필요합니다.");
                        }
                    }
                }else if (hit2.collider.CompareTag("SpaceDoor"))
                {
                    if (!hit2.collider.GetComponent<SpaceDoorOpen>().opned)
                    {
                        hit2.collider.GetComponent<SpaceDoorOpen>().Open();
                        UIManager.Instance.tooltipUI.SetText("F를 눌러 우주선 문 열기");
                    }
                    else
                    {
                        UIManager.Instance.tooltipUI.SetText("BBASS와 대화 후 우주선 밖으로 나가세요");
                    }
                }
            }
            else
            {
                UIManager.Instance.tooltipUI.Hide();
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
                    UIManager.Instance.StastUI.SetActive(false);
                    UIManager.Instance.ProductUI.SetActive(true);
                    UIManager.Instance.ProductSlotUI.SetActive(true);
                    UIManager.Instance.InvneoryUI.SetActive(true);
                    UIManager.Instance.InvneoryUI.transform.localPosition = new Vector3(330, 1f, 0);
                    UIManager.Instance.QuitSlotUI.SetActive(true);
                    GameManager.Instance.MouseCursor(true);
                    goToCrafting = true;
                }
                    
            }

            if (backToCrafting)
            {
                if (Camera.main == null) return;
                Camera.main.transform.SetParent(transform);
                Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, CameraOriginalPosition, 3 * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, CameraOriginalRotation, 150 * Time.deltaTime);
                if (Camera.main.transform.localPosition == CameraOriginalPosition &&
                    Camera.main.transform.rotation == CameraOriginalRotation)
                {
                    backToCrafting = false;
                    goToCrafting = false;
                    isCrafting = false;
                    UIManager.Instance.QuitSlotUI.SetActive(true);
                    GameManager.Instance.ismove = true;
                    GameManager.Instance.isCamera = true;
                    GameManager.Instance.noESC = false;
                    if (GameManager.Instance.nipperMake)
                    {
                        var dialogTexts = new List<DialogData>();
                        dialogTexts.Add(new DialogData("니퍼를 성공적으로 제작했습니다."));
                        dialogTexts.Add(new DialogData("우주선 밖으로 나가 전력 분배기를 수리해 보세요!"));
                        GameManager.Instance.BBASS.Show(dialogTexts);
                        GameManager.Instance.nipperMake = false;
                        GameManager.Instance.nipperMakePlay = true;
                    }

                    if (GameManager.Instance.mongkiMake)
                    {
                        var dialogTexts = new List<DialogData>();
                        dialogTexts.Add(new DialogData("몽키스페너를 성공적으로 제작했습니다."));
                        dialogTexts.Add(new DialogData("우주선 밖으로 나가 추진 제어판를 수리해 보세요!"));
                        GameManager.Instance.BBASS.Show(dialogTexts);
                        GameManager.Instance.mongkiMake = false;
                        GameManager.Instance.mongkiMakePlay = true;
                    }
                }
            }
            
        }
       
    }
}