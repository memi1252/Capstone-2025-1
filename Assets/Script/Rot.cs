using UnityEngine;

public class Rot : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    
    
    private void Update()
    {
        // Rotate the object around its Y-axis at the specified speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
