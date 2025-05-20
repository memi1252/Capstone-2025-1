using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WireManager : MonoBehaviour
{
    public static WireManager instance;
    [SerializeField] private GameObject[] wirecontainer;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wirecontainer[Random.Range(0, wirecontainer.Length)].SetActive(true);
    }

    public TextMeshProUGUI statusText;
    
    public void Success()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "성공";
        statusText.color = Color.green;
    }
    
    public void Fail()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "실패";
        statusText.color = Color.red;
    }
}
