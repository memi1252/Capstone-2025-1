using UnityEngine;

public class OtherPanel : MonoBehaviour
{
    [SerializeField] private GameObject SoundUI;
    [SerializeField] private GameObject GameUI;

public void SoundTurn()
    {
        GameUI.SetActive(false);
        SoundUI.SetActive(true);
    }

    public void GameTurn()
    {
        SoundUI.SetActive(false);
        GameUI.SetActive(true);
    }
}
