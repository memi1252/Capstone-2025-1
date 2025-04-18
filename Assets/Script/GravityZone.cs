using System;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [SerializeField] private float movespeedAddORSub;
    [SerializeField] private float thrustPowerAddORSub;
    
    private int playerInsideCount = 0;
    private Collider _collider;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideCount++;
            if (playerInsideCount == 1)
            {
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GameManager.Instance.isSpace = false;
                GameManager.Instance.player.moveSpeed += movespeedAddORSub;
                GameManager.Instance.player.thrustPower += thrustPowerAddORSub;
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
                GameManager.Instance.player.moveSpeed -= movespeedAddORSub;
                GameManager.Instance.player.thrustPower -= thrustPowerAddORSub;
            }
        }
    }
}
