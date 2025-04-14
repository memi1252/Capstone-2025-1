using System;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    // 우주선과 플레이어의 줄 길이
    [SerializeField] public float ropeLength = 0f;
    // 줄의 최대 거리
    [SerializeField] public float maxRopeLength = 50f;
    // 루프 시작점
    [SerializeField] public GameObject loopStart;
    // 루프 끝점
    [SerializeField] public GameObject loopEnd;

    private void Update()
    {
        ropeLength = Vector3.Distance(loopStart.transform.position, loopEnd.transform.position);

        GameObject Player = GameManager.Instance.player.gameObject;
        Rigidbody playerRigidbody = Player.GetComponent<Rigidbody>();
        
        
        if (ropeLength >= maxRopeLength)
        {
            Vector3 directionBackToRope = (loopStart.transform.position - Player.transform.position).normalized;
            
            Vector3 currentVelocity = playerRigidbody.linearVelocity;
            
            if (Vector3.Dot(currentVelocity, directionBackToRope) < 0)
            {
                playerRigidbody.linearVelocity = Vector3.zero;
            }
            else
            {
                GameManager.Instance.player.isMove = true;
            }
        }
        else
        {
            GameManager.Instance.player.isMove = true;
        }
    }
}