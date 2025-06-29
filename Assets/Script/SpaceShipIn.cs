using System;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class SpaceShipIn : MonoBehaviour
{
     public SpaceShip spaceShip;
     private bool inside = false;



     private bool firstOutside = false;
     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               other.GetComponent<Rigidbody>().useGravity = true;
               GameManager.Instance.inSpaceShip = true;
               other.transform.localRotation = Quaternion.Euler(0f, other.transform.eulerAngles.y, 0f);
               UIManager.Instance.BBASSViewUI.SetActive(false);
          }
     }

     private void OnTriggerStay(Collider other)
     {
          if (other.tag == "Player")
          {
               other.GetComponent<Rigidbody>().useGravity = true;
               GameManager.Instance.inSpaceShip = true;
               UIManager.Instance.BBASSViewUI.SetActive(false);
          }
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.tag == "Player")
          {
               other.GetComponent<Rigidbody>().useGravity = false;
               GameManager.Instance.inSpaceShip = false;
               UIManager.Instance.BBASSViewUI.SetActive(true);
               if(Camera.main != null)
               {
                    Camera.main.transform.localRotation = Quaternion.Euler(0f, Camera.main.transform.localRotation.y, 0f);
               }
               other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

               if (!firstOutside && spaceShip != null)
               {
                    firstOutside = true;
                    FristOutSide();
               }
          }
     }

     private void FristOutSide()
     {
          var dialogTexts = new List<DialogData>();

          dialogTexts.Add(new DialogData("저는 밖으로 나갈수 없어 무선통신으로 도와드리겠습니다."));
        
          spaceShip.Show(dialogTexts);
     }
}
