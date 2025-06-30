using System;
using UnityEngine;

public class GravityOffset : MonoBehaviour
{
    [SerializeField] public GameObject RopeObject;

    private int playerInsideCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideCount++;
            if (playerInsideCount == 1)
            {
                GameManager.Instance.isSpace = false;
                RopeObject.SetActive(false);
                GameManager.Instance.inSpaceShip = true;
                GameManager.Instance.player.GetComponent<Rigidbody>().useGravity = true;
                GameManager.Instance.player.transform.localRotation = Quaternion.Euler(0f, other.transform.eulerAngles.y, 0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideCount--;
            if (playerInsideCount <= 0)
            {
                GameManager.Instance.isSpace = true;
                RopeObject.SetActive(true);
                GameManager.Instance.inSpaceShip = false;
                GameManager.Instance.player.GetComponent<Rigidbody>().useGravity = false;
                if(Camera.main != null)
                {
                    Camera.main.transform.localRotation = Quaternion.Euler(0f, Camera.main.transform.localRotation.y, 0f);
                }
                other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            }
        }
    }
}