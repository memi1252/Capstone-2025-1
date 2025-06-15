using System;
using UnityEngine;

public class BBAAbattery : MonoBehaviour
{
    Renderer rend;

    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    
    private bool insie;
    public bool charing = false;
    bool ispos1 = false;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        var material = rend.materials[1];
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 1f));
    }

    private Color32 batteryOn = new Color32(54, 191, 191, 255);


    private void Update()
    {
        if (insie)
        {
            var material = rend.materials[1];
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", batteryOn);
        }
        else
        {
            var material = rend.materials[1];
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 1f));
        }

        if (charing)
        {
            Charing();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BBASS"))
        {
            
            insie = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BBASS"))
        {
            
            insie = false;
        }
    }

    private void Charing()
    {
        GameObject BBASS = GameObject.FindGameObjectWithTag("BBASS").transform.GetChild(0).gameObject;
        BBASS.GetComponent<Animator>().enabled = false;
        BBASS = BBASS.transform.parent.gameObject;
        if (BBASS != null)
        {
            if (BBASS.transform.position != pos1.transform.position && !ispos1)
            {
                BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position, pos1.transform.position,
                    2f * Time.deltaTime);
                BBASS.transform.LookAt(pos1.transform.position);
            }
            else
            {
                if (BBASS.transform.rotation != pos1.transform.rotation)
                {
                    BBASS.transform.rotation = Quaternion.RotateTowards(BBASS.transform.rotation,
                        pos1.transform.rotation, 100f * Time.deltaTime);
                }
                else
                {
                    ispos1 = true;
                    if (BBASS.transform.position != pos2.transform.position)
                    {
                        BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position,
                            pos2.transform.position, 1f * Time.deltaTime);
                    }
                    else
                    {
                        charing = false;
                    }
                }
            }
        }
    }
}
