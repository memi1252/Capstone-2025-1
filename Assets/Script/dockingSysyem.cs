using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class dockingSysyem : MonoBehaviour
{
    [SerializeField] private float movdeSpeed = 1;
    [SerializeField] private GameObject lookCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject SpaceShip;
    [SerializeField] private Vector3 PlayerPos;
    [SerializeField] private Quaternion PlayerRot;
    [SerializeField] private Image transparency;
    [SerializeField] private Image helpImage;
    [SerializeField] private GameObject dockingstationArrow;
    [SerializeField] private Image TimerBar;
    [SerializeField] private float maxTime;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI ButtonClickTimeText; 
    [SerializeField] private float ButtonClickMaxTime = 5f;
    private float ButtonClickCurrentTime;

    private float currentTime;

    private float h;
    private float v;
    private float ud;
    private float mouseX;
    private float mouseY;
    private float xRotation;
    private float yRotation;

    public bool ismove;
    private float a =1;
    private bool SpaceON;
    public SPACESTART ss;


    private Vector3 originPos;
    private Quaternion originRot;


    private void Start()
    {
        UIManager.Instance.tooltipUI.gameObject.SetActive(false);
        currentTime = maxTime;
        originPos = transform.position;
        originRot = transform.rotation;
        ButtonClickCurrentTime = ButtonClickMaxTime;
    }

    public void Init()
    {
        transform.position = originPos;
        transform.rotation = originRot;
        helpImage.gameObject.SetActive(true);
        GameManager.Instance.MouseCursor(true);
        SpaceON = false;
        a = 1;
        ismove = false;
        currentTime = maxTime;
        TimerBar.fillAmount = currentTime / maxTime;
        transparency.enabled = true;
        ButtonClickCurrentTime = ButtonClickMaxTime;
        dockingstationArrow.SetActive(true);
    }

    private void GameOver()
    {
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        if (ss != null)
        {
            ss.transform.parent.gameObject.SetActive(true);
            
            Debug.Log("dd");
        }
        GameManager.Instance.noInventoryOpen = false;
        GameManager.Instance.player.gameObject.SetActive(true);
    }


    public void close()
    {
        helpImage.gameObject.SetActive(false);
        GameManager.Instance.MouseCursor(false);
        SpaceON = true;
    }

    private void Update()
    {
        
        if (ButtonClickCurrentTime >= 0)
        {
            ButtonClickCurrentTime -= Time.deltaTime;
            ButtonClickTimeText.text = $"{(int)ButtonClickCurrentTime}초후 버튼이 활성화 됩니다.";
        }
        else
        {
            closeButton.interactable = true;
            ButtonClickTimeText.gameObject.SetActive(false);
        }
        
        
        if (Vector3.Distance(transform.position, dockingstationArrow.transform.position) < 20f)
        {
            dockingstationArrow.SetActive(false);
        }
        
        // 스킵
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(GetComponentInChildren<Canvas>() != null)
                GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            foreach (var varCamera in GetComponentsInChildren<Camera>())
            {
                varCamera.enabled = false;
            }
            GetComponent<MeshRenderer>().enabled = false;
            SpaceShip.SetActive(true);
            GameObject BBASS = GameObject.FindGameObjectWithTag("BBASS");
            BBASS.GetComponent<BBASS_Ment1>().enabled = false;
            BBASS.GetComponent<BBASS_Ment2>().enabled = true;
            SpaceShip.GetComponent<Animator>().SetTrigger("Docking");
            StartCoroutine(spaceShipToPlayer());
        }
        
        if(transparency != null && SpaceON)
        {
            a -= Time.deltaTime * 0.3f;
            if (transparency.color.a > 0.3f)
            {
                transparency.color = new Color(0, 0, 0, a);
            }
            else
            {
                transparency.enabled = false;
                ismove = true;
                SpaceON = false;
                a = 0;
            }
        }
        if(!ismove) return;
        Move();
        CameraLook();
        currentTime -= Time.deltaTime;
        TimerBar.fillAmount = currentTime / maxTime;
        if((int)currentTime/60 > 0)
        {
            TimerText.text = $"{(int)currentTime / 60}분 {(int)currentTime % 60}초 남음";
        }
        else
        {
            TimerText.text = $"{(int)currentTime % 60}초 남음";
        }
        if(currentTime <= 0)
        {
            transparency.enabled = true;
            a += Time.deltaTime * 0.7f;
            if (transparency.color.a < 0.9f)
            {
                transparency.color = new Color(0, 0, 0, a);
            }
            else
            {
                transparency.enabled = false;
                ismove = false;
                GameOver();
                Init();
                gameObject.SetActive(false);
            }
        }
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        ud = Input.GetAxis("upanddown");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movdeSpeed = 2f;
        }
        else
        {
            movdeSpeed = 1f;
        }
        
        Vector3 moveDirection = new Vector3(h, ud, v).normalized;
        
        transform.Translate(moveDirection * Time.deltaTime * movdeSpeed);
    }
    
    private void CameraLook()
    {
        if (lookCamera != null)
        {
            mouseX = Input.GetAxis("Mouse X") *0.3f;
            mouseY = Input.GetAxis("Mouse Y") * 0.5f;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);
            
            transform.Rotate(Vector3.up * mouseX);
            lookCamera.transform.eulerAngles = new Vector3(xRotation, lookCamera.transform.rotation.eulerAngles.y, 0f);
            lookCamera.transform.rotation = Quaternion.Euler(
                lookCamera.transform.rotation.eulerAngles.x,
                lookCamera.transform.rotation.eulerAngles.y,
                0f
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dockingstation"))
        {
            Debug.Log("도킹완료");
            if(GetComponentInChildren<Canvas>() != null)
                GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            foreach (var varCamera in GetComponentsInChildren<Camera>())
            {
                varCamera.enabled = false;
            }
            GetComponent<MeshRenderer>().enabled = false;
            SpaceShip.SetActive(true);
            GameObject BBASS = GameObject.FindGameObjectWithTag("BBASS");
            BBASS.GetComponent<BBASS_Ment1>().enabled = false;
            BBASS.GetComponent<BBASS_Ment2>().enabled = true;
            SpaceShip.GetComponent<Animator>().SetTrigger("Docking");
            StartCoroutine(spaceShipToPlayer());
        }
    }

    IEnumerator spaceShipToPlayer()
    {
        yield return new WaitForSeconds(4f);
        SpaceShip.GetComponentInChildren<Camera>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        player.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.itemDescriptionUI.SetActive(false);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        player.transform.position = PlayerPos;
        player.transform.rotation = PlayerRot;
        GameManager.Instance.MouseCursor(false);
        gameObject.SetActive(false);
        GameObject BBASS = GameObject.FindGameObjectWithTag("BBASS");
        BBASS.transform.position = new Vector3(2,18,15.5539999f);
        BBASS.transform.rotation = Quaternion.Euler(0,180,0);
        GameManager.Instance.noInventoryOpen = false;
        
    }
}
