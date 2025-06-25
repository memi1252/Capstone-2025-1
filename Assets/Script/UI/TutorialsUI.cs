using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsUI : MonoBehaviour
{
    [SerializeField] private Image w;
    [SerializeField] private Image a;
    [SerializeField] private Image s;
    [SerializeField] private Image d;
    [SerializeField] private Image shift;
    [SerializeField] private Image space;
    [SerializeField] private Image f;
    [SerializeField] private Image I;
    

    [SerializeField] private Sprite Holdw;
    [SerializeField] private Sprite Holda;
    [SerializeField] private Sprite Holds;
    [SerializeField] private Sprite Holdd;
    [SerializeField] private Sprite Holdshift;
    [SerializeField] private Sprite Holdspace;
    [SerializeField] private Sprite Holdf;
    [SerializeField] private Sprite HoldI;

    [SerializeField] public GameObject move;
    [SerializeField] public GameObject interaction;


    private bool ismove = true;
    private bool isinteract = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) &&!ismove)
        {
            w.sprite = Holdw;
        }
        if (Input.GetKeyDown(KeyCode.A) && !ismove)
        {
            a.sprite = Holda;
        }
        if (Input.GetKeyDown(KeyCode.S)&& !ismove)
        {
            s.sprite = Holds;
        }
        if (Input.GetKeyDown(KeyCode.D)&&!ismove)
        {
            d.sprite = Holdd;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&&!ismove)
        {
            shift.sprite = Holdshift;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&!ismove)
        {
            space.sprite = Holdspace;
        }

        if (!ismove && w.sprite == Holdw && a.sprite == Holda && s.sprite == Holds && d.sprite == Holdd && shift.sprite == Holdshift && space.sprite == Holdspace)
        {
            ismove = true;
            StartCoroutine(Moveoff());
        }
        
        if (Input.GetKeyDown(KeyCode.F) && !isinteract)
        {
            f.sprite = Holdf;
        }
        if (Input.GetKeyDown(KeyCode.I) && !isinteract)
        {
            I.sprite = HoldI;
        }
        
        if(!isinteract&& f.sprite == Holdf && I.sprite == HoldI)
        {
            StartCoroutine(interactionoff());
        }
        
    }

    public void MoveOn()
    {
        move.SetActive(true);
        ismove = false;
    }

    IEnumerator Moveoff()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        move.SetActive(false);
        interaction.SetActive(true);
        isinteract = false;
    }

    IEnumerator interactionoff()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        interaction.SetActive(false);
        
    }
}
