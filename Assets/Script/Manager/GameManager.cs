using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player")]   
    [SerializeField] public Player player;
    [SerializeField] public PlayerCamera playerCamera;

    public bool isSpace = true;
    public bool ismove = true;
    public bool isCamera;

    private void Update()
    {
        //임시
        if (Input.GetKeyDown(KeyCode.G))
        {
            //미니게임 전선연결 오픈
            UIManager.Instance.wireManager.Show();
        }
    }
}
