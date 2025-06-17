using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class BBAAbattery : BBASS_MentBASE
{
    Renderer rend;
    [SerializeField] private SPACESTART spaceStart;
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    
    private bool insie;
    public bool charing = false;
    public bool isCharing = false;
    bool ispos1 = false;

    private GameObject BBASS;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        var material = rend.materials[1];
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 1f));
        BBASS = GameObject.FindGameObjectWithTag("BBASS").gameObject;
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
            charge();
        }

        if (isCharing)
        {
            ChargingComplete();
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

    private void charge()
    {
        BBASS.GetComponentInChildren<Animator>().enabled = false;
        foreach (var col in BBASS.GetComponents<BoxCollider>())
        {
            if (col.isTrigger)
            {
                col.enabled = false;
            }
        }
        
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
                        Charging();
                    }
                }
            }
        }
    }
    
    private bool isFirst = true;
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        GameManager.Instance.MouseCursor(true);
        
        yield return StartCoroutine(base.PrintDialogList(dataList));

        if (isFirst)
        {
            isFirst = false;
            StartCoroutine(nextMove());
        }
        GameManager.Instance.MouseCursor(false);
        
        Printer.SetActive(false);
    }
    
    private void Charging()
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("충전중!"));
        Show(dialogTexts);
    }

    IEnumerator nextMove()
    {
        yield return new WaitForSeconds(5f);
        isCharing = true;
    }

    
    private bool completed = false;

    private void ChargingComplete()
    {
        BBASS.GetComponentInChildren<Animator>().enabled = false;
        if (BBASS != null)
        {
            if (BBASS.transform.position != pos1.transform.position && ispos1)
            {
                BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position, pos1.transform.position,
                    1f * Time.deltaTime);
            }
            else
            {
                ispos1 = false;
                if (!completed)
                {
                    var dialogTexts = new List<DialogData>();
                    dialogTexts.Add(new DialogData("충전이 완료되었습니다."));
                    Show(dialogTexts);
                    completed = true;
                }
                if (BBASS.transform.position != spaceStart.pos1.transform.position)
                {
                    BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position, spaceStart.pos1.transform.position, 2f * Time.deltaTime);
                    BBASS.transform.LookAt(spaceStart.pos1.transform.position);
                }
                else
                {
                    if (BBASS.transform.rotation != spaceStart.pos1.transform.rotation)
                    {
                        BBASS.transform.rotation = Quaternion.RotateTowards(BBASS.transform.rotation, spaceStart.pos1.transform.rotation, 100f * Time.deltaTime);
                    }
                    else
                    {
                        completed = false;
                        isCharing = false;
                        foreach (var col in BBASS.GetComponents<BoxCollider>())
                        {
                            if (col.isTrigger)
                            {
                                col.enabled = true;
                            }
                        }
                    }
                }
            }
        }
        
    }
}
