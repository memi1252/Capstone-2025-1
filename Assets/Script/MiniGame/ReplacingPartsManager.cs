using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReplacingPartsManager : MonoBehaviour
{
    public static ReplacingPartsManager instance;
    public PartPointer takePart;
    public PartPointer[] parts;
    public bool success;
    public TextMeshProUGUI statusText;
    
    public void Awake()
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

    private void Update()
    {
        if (!success)
        {
            foreach (PartPointer p in parts)
            {
                if(p.insPartPointer == null) break;
                if (p.value != p.insPartPointer.value) break;
                if(p == parts[parts.Length - 1])
                {
                    success = true;
                    Success();
                }
            }
        }
        
    }
    
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
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.QuestUI.SetActive(true);
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        GameManager.Instance.miniGameScene = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.ReplacingPartsScene);
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
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.QuestUI.SetActive(true);
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        GameManager.Instance.miniGameScene = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.ReplacingPartsScene);
    }
}
