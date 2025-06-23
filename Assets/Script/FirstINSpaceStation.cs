using System;
using UnityEngine;

public class FirstINSpaceStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.dayContViewUI.DayCountPlay(1);
            gameObject.SetActive(false);
        }
    }
}
