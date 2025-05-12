using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; 
    
    private bool isMouseOver = false;
    private bool isDragging = false;
    private bool success = false;

    public bool ss;
    
    
    
    private void Start()
    {
        
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
    }

    private void RotateObject()
    {
        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        
        transform.Rotate(Vector3.down, rotationX, Space.World);
        if (ss)
        {
            transform.Rotate(Vector3.right, rotationY, Space.World); 
        }
        else
        {
            transform.Rotate(Vector3.left, rotationY, Space.World); 
        }
    }

    private void OnMouseDown()
    {
        if (isMouseOver) 
        {
            isDragging = true;
        }
    }

    private void OnMouseOver()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}