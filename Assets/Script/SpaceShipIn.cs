using System;
using UnityEngine;

public class SpaceShipIn : MonoBehaviour
{
     private bool inside = false;
     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               other.GetComponent<Rigidbody>().useGravity = true;
               GameManager.Instance.inSpaceShip = true;
               other.transform.localRotation = Quaternion.Euler(0f, other.transform.eulerAngles.y, 0f);
          }
     }

     private void OnTriggerStay(Collider other)
     {
          if (other.tag == "Player")
          {
               other.GetComponent<Rigidbody>().useGravity = true;
               GameManager.Instance.inSpaceShip = true;
          }
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.tag == "Player")
          {
               other.GetComponent<Rigidbody>().useGravity = false;
               GameManager.Instance.inSpaceShip = false;
          }
     }
}
