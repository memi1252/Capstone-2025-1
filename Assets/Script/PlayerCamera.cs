using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    private Transform playerBody;
    private float xRotation = 0f;
    public float mouseX;
    public float mouseY;

    private void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        if(!GameManager.Instance.isSpace)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
            
        }
        else if (GameManager.Instance.isSpace)
        {
            HandleCameraRotation(mouseX, mouseY);
        }
    }
    
    
    void HandleCameraRotation(float mouseX, float mouseY)
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 각도 제한
        
            
        // 1인칭 카메라 (플레이어 머리에 고정)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            
        // 플레이어 회전도 함께 적용
        playerBody.Rotate(Vector3.up * mouseX);
    }
    
}
