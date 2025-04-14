using System;
using UnityEngine;

public class GravityOffset : MonoBehaviour
{
    [SerializeField] public GameObject RopeObject;

    private int playerInsideCount = 0; // 플레이어가 이 오브젝트 안에 머무는 콜라이더 개수 추적

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideCount++;

            // 처음 들어왔을 때만 처리
            if (playerInsideCount == 1)
            {
                GameManager.Instance.isSpace = false;
                Vector3 eulerAngles = other.transform.eulerAngles;
                other.transform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
                RopeObject.SetActive(false);

                if (eulerAngles.x >= -90 && eulerAngles.x <= 90)
                    GameManager.Instance.playerCamera.transform.localRotation = Quaternion.Euler(eulerAngles.x, 0, 0);
                else
                    GameManager.Instance.playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            // 중력 항상 켬
            Rigidbody rd = other.GetComponent<Rigidbody>();
            rd.useGravity = true;
        }
    }

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
            playerInsideCount--;

            // 다 나갔을 때만 처리
            if (playerInsideCount <= 0)
            {
                Rigidbody rd = other.GetComponent<Rigidbody>();
                rd.useGravity = false;
                rd.linearVelocity = Vector3.zero;
                GameManager.Instance.isSpace = true;
                RopeObject.SetActive(true);
            }
        }
    }
}