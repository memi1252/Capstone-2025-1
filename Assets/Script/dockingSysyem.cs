using System;
using UnityEngine;

public class dockingSysyem : MonoBehaviour
{
    [SerializeField] private float movdeSpeed = 1;
    [SerializeField] private GameObject lookCamera;

    private float h;
    private float v;
    private float ud;
    private float mouseX;
    private float mouseY;
    private float xRotation;
    private float yRotation;
    
    private void Update()
    {
        Move();
        CameraLook();
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        ud = Input.GetAxis("upanddown");
        
        Vector3 moveDirection = new Vector3(h, ud, v).normalized;
        
        transform.Translate(moveDirection * Time.deltaTime * movdeSpeed);
    }
    
    private void CameraLook()
    {
        if (lookCamera != null)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            
            
            
            transform.Rotate(Vector3.up * mouseX);
            lookCamera.transform.eulerAngles = new Vector3(xRotation, lookCamera.transform.rotation.eulerAngles.y, 0f);
            lookCamera.transform.rotation = Quaternion.Euler(
                lookCamera.transform.rotation.eulerAngles.x,
                lookCamera.transform.rotation.eulerAngles.y,
                0f
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dockingstation"))
        {
            Debug.Log("도킹완료");
            GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            foreach (var varCamera in GetComponentsInChildren<Camera>())
            {
                varCamera.enabled = false;
                
            }
            
        }
    }
}
