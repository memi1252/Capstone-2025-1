using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; 
    
    [SerializeField] private int absolute;
    [SerializeField] private float smoothTime = 0.1f; 
    
    private bool isMouseOver = false;
    private bool isDragging = false;
    private bool success = false;
    
    private int x;
    private int y;
    private int z;
    
    private void Start()
    {
        x = Random.Range(-180, 180);
        y = Random.Range(-180, 180);
        z = Random.Range(-180, 180);
        transform.rotation = Quaternion.Euler(x, y, z);
        
    }
    
    private Quaternion currentRotation;
    
    private void Update()
    {
        if (isDragging && Input.GetMouseButton(0) && !success) 
        {
            RotateObject();
        }
        else if (Input.GetMouseButtonUp(0) && !success) 
        {
            isDragging = false; 
            isMouseOver = false;
        }
        
        if (Mathf.Abs(transform.rotation.eulerAngles.x) < absolute &&
            Mathf.Abs(transform.rotation.eulerAngles.y) < absolute &&
            Mathf.Abs(transform.rotation.eulerAngles.z) < absolute &&
            !success) 
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("DD");
            success = true;
        }

        if (success)
        {
            currentRotation = transform.rotation;
            transform.rotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(0, 0, 0), smoothTime * Time.deltaTime);
        }

        if (success && gameObject.transform.rotation.eulerAngles == new Vector3(0, 0, 0))
        {
            UIManager.Instance.RotaionGame.Hide();
        }
    }

    private void RotateObject()
    {
        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        
        transform.Rotate(Vector3.down, rotationX, Space.World); 
        transform.Rotate(Vector3.right, rotationY, Space.World); 
    }

    private void OnMouseDown()
    {
        if (isMouseOver) 
        {
            isDragging = true;
        }
    }

    private void OnMouseEnter()
    {
        isMouseOver = true; 
    }
}