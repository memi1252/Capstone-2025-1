using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    private Transform playerBody;
    private float xRotation = 0f;
    

    private void Start()
    {
        playerBody = transform.parent;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (GameManager.Instance.isCamera)
        {
            if (GameManager.Instance.inSpaceShip)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -80f, 80f);
                playerBody.transform.Rotate(Vector3.up * mouseX, Space.Self);
            
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }
            else
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -80f, 80f);
                playerBody.transform.Rotate(Vector3.up * mouseX, Space.Self);
            
                Vector3 bodyRotation = playerBody.eulerAngles;
                playerBody.rotation = Quaternion.Euler(bodyRotation.x, bodyRotation.y, 0f);

                if (Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    xRotation = 0;
                }
            
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                }
                else
                {
                    playerBody.transform.Rotate(Vector3.left * mouseY, Space.Self);
                }
            }
            
        }
    }
    
    
    
    
}
