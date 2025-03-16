using System;
using UnityEngine;

public class GravityOffset : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rd = other.GetComponent<Rigidbody>();
            rd.useGravity = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rd = other.GetComponent<Rigidbody>();
            rd.useGravity = false;
            GameManager.Instance.isSpace = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.isSpace = false; 
            Vector3 eulerAngles = other.transform.eulerAngles;
            other.transform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
            if(eulerAngles.x >= -90 && eulerAngles.x <= 90)
                GameManager.Instance.playerCamera.transform.localRotation = Quaternion.Euler(eulerAngles.x, 0, 0);
            else 
                GameManager.Instance.playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }   
    }
}
