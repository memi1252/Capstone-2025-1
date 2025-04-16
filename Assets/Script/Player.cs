using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float jumpDistance = 1.1f;
    [SerializeField] public float thrustPower = 5f;  // 이동 속도 (추진력)
    [SerializeField] private float rotationSpeed = 2f; // 회전 속도
    [SerializeField] private float rollSpeed = 50f; // Q, E 키로 회전하는 속도
    [SerializeField] private float boostMultiplier = 2f; // 부스터 속도 배율
    [SerializeField] private float ItemPickUpDistance; // 아이템 획득 거리
    [SerializeField] private GameObject Rope;
    [SerializeField] public bool isMove = true;
    private bool isjump = false;

    
    
    
    
    private Rigidbody rigidbody;

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
            HandleRotation();
            HandleRoll();
            ItemPickUp();
        }
    }
    
    
    void HandleMovement()
    {
        if (isMove)
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

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.up * mouseX, Space.World);
        transform.Rotate(Vector3.left * mouseY, Space.Self);
    }
    
    void HandleRoll() // Q, E 키로 캐릭터 기울이기
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward * rollSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back * rollSpeed * Time.deltaTime, Space.Self);
        }
    }
    
    void ItemPickUp()
    {
        // 아이템 획득
        if (Input.GetMouseButtonDown(0))
        {
            Transform cameraTransform = Camera.main.transform;
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * ItemPickUpDistance, Color.red);
            bool isPickUp = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit,
                ItemPickUpDistance);
            if (isPickUp)
            {
                if (hit.collider.CompareTag("Item"))
                {
                    Item item = hit.transform.GetComponent<Item>();
                    Debug.Log("아이템 이름 : " + item.ItemName + " 아이템 무게 : " + item.ItemWeight + " 아이템 갯수 : " + item.ItemCount + " 아이템 아이디 : " + item.ItemId);
                    // 아이템 획득
                    Debug.Log("아이템 획득");
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}