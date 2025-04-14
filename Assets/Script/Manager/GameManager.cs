using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player")]   
    [SerializeField] public Player player;
    [SerializeField] public PlayerCamera playerCamera;

    public bool isSpace = true;
    public bool ismove = true;
    public bool isCamera;
}
