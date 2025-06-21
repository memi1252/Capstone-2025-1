using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplacingPartsManager : MonoBehaviour
{
    public static ReplacingPartsManager instance;
    public bool isStart = false;
    public PartPointer takePart;
    public PartPointer[] parts;
    public bool success;
    public TextMeshProUGUI statusText;
    public Image TimerImage;
    public float TImer;
    public GameObject helpImage;
    private float currentTime;

    public GameObject line;
    
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

    public void GameStart()
    {
        isStart = true;
        GameManager.Instance.DirectionalLight.SetActive(true);
        helpImage.SetActive(false);
    }

    private void Update()
    {
        if (isStart)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= TImer)
            {
                TimerImage.fillAmount = 0f;
                if (!success)
                {
                    Fail();
                }
            }
            else
            {
                TimerImage.fillAmount = 1- (currentTime / TImer);
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
        }
    }
    
    public void Success()
    {
        var material = line.GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.green * 2f);
        // statusText.gameObject.SetActive(true);
        // statusText.text = "성공";
        // statusText.color = Color.green;
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
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        GameManager.Instance.miniGameScene = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.ReplacingPartsScene);
    }
    
    public void Fail()
    {
        var material = line.GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.black * 2f);
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
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        GameManager.Instance.miniGameScene = true;
        SceneManager.UnloadSceneAsync(GameManager.Instance.ReplacingPartsScene);
    }
}
