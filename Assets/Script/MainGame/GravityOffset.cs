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
                other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            }
        }
    }
}