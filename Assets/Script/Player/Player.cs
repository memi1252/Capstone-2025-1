using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEditor;
using UnityEngine;

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
            //HandleRotation();
            //HandleRoll();
            
        }
        PickUpItem();
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

        transform.Rotate(Vector3.up * mouseX, Space.Self);
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

    private item lookAtItem;
    void PickUpItem()
    {
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
                    item.GetComponent<Renderer>().materials[1].SetFloat("_outlien_thickness", 0.01f);
                }
                if (Input.GetKeyDown(KeyCode.F) && !item.isFrontItem)
                {
                    item.frontitem(hit.collider.gameObject);
                }
                if (Input.GetKeyDown(KeyCode.F) && item.isRotateItem)
                {
                    item.Pickup();
                }
                UIManager.Instance.tooltipUI.SetText(item.itemName);
            }
            else
            {
                if (lookAtItem != null)
                {
                    lookAtItem.outline = false;
                    lookAtItem.GetComponent<Renderer>().materials[1].SetFloat("_outlien_thickness", 0.0f);
                }
                lookAtItem = null;
            }
            
        }else
        {
            if (lookAtItem != null)
            {
                lookAtItem.outline = false;
                lookAtItem.GetComponent<Renderer>().materials[1].SetFloat("_outlien_thickness", 0.0f);
            }
            lookAtItem = null;
            UIManager.Instance.tooltipUI.Hide();
        }
    }
}