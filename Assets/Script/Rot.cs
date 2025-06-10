using UnityEngine;

public class Rot : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    public Vector3 rotationAxis = Vector3.up;
    
    
    private void Update()
    {
        // Rotate the object around its Y-axis at the specified speed
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
