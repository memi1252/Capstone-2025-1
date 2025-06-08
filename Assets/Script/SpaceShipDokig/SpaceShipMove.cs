using UnityEngine;

public class SpaceShipMove : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float moveSpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward * Time.deltaTime * moveSpeed; // 전진
            if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward * Time.deltaTime * moveSpeed; // 후진
            if (Input.GetKey(KeyCode.A)) moveDirection -= transform.right * Time.deltaTime * moveSpeed; // 좌측 이동
            if (Input.GetKey(KeyCode.D)) moveDirection += transform.right * Time.deltaTime * moveSpeed; // 우측 이동
            if (Input.GetKey(KeyCode.Space)) moveDirection += transform.up * Time.deltaTime * moveSpeed; // 상승
            if (Input.GetKey(KeyCode.LeftControl)) moveDirection -= transform.up * Time.deltaTime * moveSpeed; // 하강
    }
}
