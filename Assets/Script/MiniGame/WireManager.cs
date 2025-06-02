using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //wirecontainer[Random.Range(0, wirecontainer.Length)].SetActive(true);
    }

    public TextMeshProUGUI statusText;
    
    public void Success()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "성공";
        statusText.color = Color.green;
        StartCoroutine(SuccessCoroutine());
    }
    
    IEnumerator SuccessCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.MinMapUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.QuestUI.SetActive(true);
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        GameManager.Instance.miniGameScene = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
    }
    
    public void Fail()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "실패";
        statusText.color = Color.red;
        StartCoroutine(FailCoroutine());
    }
    IEnumerator FailCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.MinMapUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.QuestUI.SetActive(true);
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        GameManager.Instance.miniGameScene = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
    }
}
