using System;
using System.Collections;
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

    private void Start()
    {
        UIManager.Instance.tooltipUI.gameObject.SetActive(false);
    }


    public void close()
    {
        helpImage.gameObject.SetActive(false);
        GameManager.Instance.MouseCursor(false);
        SpaceON = true;
    }

    private void Update()
    {
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
            BBASS.transform.position = new Vector3(2,18,15.5539999f);
            BBASS.transform.rotation = Quaternion.Euler(0,180,0);
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
                transparency = null;
            }
        }
        if(!ismove) return;
        Move();
        CameraLook();
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
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            
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
            BBASS.transform.position = new Vector3(2,18,15.5539999f);
            BBASS.transform.rotation = Quaternion.Euler(0,180,0);
            BBASS.GetComponent<BBASS_Ment1>().enabled = false;
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
        UIManager.Instance.itemDescriptionUI.Hide();
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        player.transform.position = PlayerPos;
        player.transform.rotation = PlayerRot;
        GameManager.Instance.MouseCursor(false);
        gameObject.SetActive(false);
        
    }
}
