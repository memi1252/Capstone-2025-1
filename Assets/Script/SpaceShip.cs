using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;



public class SpaceShip : BBASS_MentBASE
{
    [SerializeField] private GameObject posDoorFront;

    public bool isDoorFront;

    private GameObject BBASS;
    
    private int currentTargetIndex = 0;

    private void Start()
    {
        BBASS = GameObject.FindGameObjectWithTag("BBASS");
    }

    private void Update()
    {
        if (isDoorFront)
        {
            if(BBASS.transform.position!= posDoorFront.transform.position)
            {
                BBASS.transform.position = Vector3.MoveTowards(BBASS.transform.position, posDoorFront.transform.position, 3f * Time.deltaTime);
                BBASS.transform.LookAt(posDoorFront.transform.position);
            }
            else
            {
                if (BBASS.transform.rotation != posDoorFront.transform.rotation)
                {
                    BBASS.transform.rotation = Quaternion.RotateTowards(BBASS.transform.rotation, posDoorFront.transform.rotation, 100f * Time.deltaTime);
                }
                else
                {
                    if (DoorFrontMent)
                    {
                        DoorFront();
                        DoorFrontMent = false;
                        isDoorFront = false;
                    }
                }
            }
        }
    }
    
    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        yield return StartCoroutine(base.PrintDialogList(dataList));
        
        Printer.SetActive(false);
        FindAnyObjectByType<SpaceDoorOpen>().isOpen = true;
    }

    private bool DoorFrontMent = true;
    private void DoorFront()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("이문을 열고 나가시면 바로 밑에 우주정거장 입구가 있습니다!"));
        
        Show(dialogTexts);
    }

    
}
