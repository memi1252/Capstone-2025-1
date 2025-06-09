using UnityEngine;

public class SpaceShipMove : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection;
     Vector3 dir;
    private Rigidbody rigidbody;

    [SerializeField] public float thrustPower = 5f;  // 이동 속도 (추진력)
        
    void Update()   
    {
        HandleMovement();
    }
        
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
          
        rigidbody.freezeRotation = true;

    }
        
        
          
    void HandleMovement()
    {
        // if (isMove)
        // {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward * Time.deltaTime * moveSpeed; // 전진
            if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward * Time.deltaTime * moveSpeed; // 후진
            if (Input.GetKey(KeyCode.A)) moveDirection -= transform.right * Time.deltaTime * moveSpeed; // 좌측 이동
            if (Input.GetKey(KeyCode.D)) moveDirection += transform.right * Time.deltaTime * moveSpeed; // 우측 이동
            if (Input.GetKey(KeyCode.Space)) moveDirection += transform.up * Time.deltaTime * moveSpeed; // 상승
            if (Input.GetKey(KeyCode.LeftControl)) moveDirection -= transform.up * Time.deltaTime * moveSpeed; // 하강


            rigidbody.AddForce(moveDirection, ForceMode.Acceleration);
        }
    }
