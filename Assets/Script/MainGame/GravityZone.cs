using System;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [SerializeField] private float movespeedAddORSub;
    [SerializeField] private float thrustPowerAddORSub;
    [SerializeField] private bool isRotate;
    
    private int playerInsideCount = 0;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GameManager.Instance.player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideCount++;
            if (playerInsideCount == 1)
            {
                GameManager.Instance.isSpace = false;
                GameManager.Instance.player.moveSpeed += movespeedAddORSub;
                GameManager.Instance.player.thrustPower += thrustPowerAddORSub;
                if (isRotate)
                {
                    _rigidbody.freezeRotation = false;
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                }
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
                if (isRotate)
                {
                    GameManager.Instance.player.transform.GetChild(0).rotation = GameManager.Instance.player.transform.rotation;
                    _rigidbody.freezeRotation = true;
                }
            }
        }
    }
}
