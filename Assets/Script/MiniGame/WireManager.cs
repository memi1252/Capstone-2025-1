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

    public bool start = false;

    

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI countText;
    private WireConnectionDoor wireConnectionDoor;

    public int count = 4;

    public float wireTimer = 60;

    private Coroutine dddd;
    
    private void Start()
    {
        //wirecontainer[Random.Range(0, wirecontainer.Length)].SetActive(true);
        audioSource = GetComponent<AudioSource>();
        foreach (var meshRenderer in Light_Bulbs)
        {
            meshRenderer.material.color = Color.white;
            var material = meshRenderer.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.white * 2f);
        }
        wireConnectionDoor = GameObject.FindGameObjectWithTag("WireConnectionDoor").GetComponent<WireConnectionDoor>();
    }
    
    private IEnumerator TurnOffLightsSequentially()
    {
        float turnOffInterval = wireTimer / Light_Bulbs.Length; // 각 전등이 꺼지는 간격 계산

        foreach (var light in Light_Bulbs)
        {
            // 전등 색상을 빨간색으로 변경 (꺼진 효과)
            light.material.color = Color.black;
            var material = light.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.black * 0.5f);

            yield return new WaitForSeconds(turnOffInterval); // 간격 대기
        }
    }
    
    public void TurnOnLights()
    {
        // 모든 전등을 켜는 코루틴 시작
        dddd = StartCoroutine(TurnOffLightsSequentially());
    }
    
    private void Update()
    {
        if (start)
        {
            countText.text = $"남은 횟수 : {count}";
            
            if(count <= 0)
            {
                Fail();
            }
        }
        
    }

    public void Success()
    {
        //statusText.color = Color.green;
        StopCoroutine(dddd);
        foreach (var meshRenderer in Light_Bulbs)
        {
            meshRenderer.material.color = Color.green;
            var material = meshRenderer.material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.green * 2f);
        }
        QuestManager.Instance.quests[5].clear = true;
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
        wireConnectionDoor.Clear = true;
        wireConnectionDoor.Close();
        UIManager.Instance.BBASSViewUI.SetActive(true);
        GameManager.Instance.noInventoryOpen = false;
        GameManager.Instance.BBASS.GetComponent<BBABB_WireCLEAR>().Clear();
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
    }
    
    public void Fail()
    {
        StopCoroutine(TurnOffLightsSequentially());
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
        wireConnectionDoor.Close();
        UIManager.Instance.BBASSViewUI.SetActive(true);
        GameManager.Instance.noInventoryOpen = false;
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
    }
}
