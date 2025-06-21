using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WireManager : MonoBehaviour
{
    public static WireManager instance;
    [SerializeField] private GameObject[] wirecontainer;
    [SerializeField] private MeshRenderer[] Light_Bulbs;
    
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    
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
        audioSource = GetComponent<AudioSource>();
    }

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI countText;

    public int count = 4;

    private void Update()
    {
        countText.text = $"남은 횟수 : {count}";
        
        if(count <= 0)
        {
            Fail();
        }
    }

    public void Success()
    {
        //statusText.color = Color.green;
        foreach (var meshRenderer in Light_Bulbs)
        {
            meshRenderer.material.color = Color.green;
            var material = meshRenderer.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.green * 2f);
        }
        audioSource.clip = audioClips[0];
        audioSource.Play();
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
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
    }
    
    public void Fail()
    {
        foreach (var meshRenderer in Light_Bulbs)
        {
            meshRenderer.material.color = Color.red;
            var material = meshRenderer.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.red * 2f);
        }
        audioSource.clip = audioClips[1];
        audioSource.Play();
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
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
    }
}
