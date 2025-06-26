using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColGo : MonoBehaviour
{
    public TDActiveElement activeParent;
    private TDScene tdscene;
    [HideInInspector]
    public bool actived;
    [HideInInspector]
    public bool hasexit;
    [Header("key")]
    public bool keyActive;

    [SerializeField] private GameObject LockDoorHelpUI;

    private bool isOpen;
    public string keycode;

    public AudioSource doorOpenSound;
    public AudioSource cardKeyDoorSound;

    private void Start()
    {
        tdscene = GameObject.FindWithTag("TdLevelManager").GetComponent<TDScene>();
    }
    void OnTriggerEnter(Collider trig)
    {
        //Debug.Log("hey! im working! cuz" + trig + "has entered and my player is: " + tdscene.PlayerChar.gameObject.GetComponent<Collider>());
        //Check character in range and keycode pressed or automatic to start action
        if (trig.GetComponent<Collider>() == tdscene.PlayerChar)
        {
            if (keyActive)
            {
                if (tdscene.PlayerChar.GetComponent<Player>().haveKeycode == null)
                {
                    cardKeyDoorSound.Play();
                    UIManager.Instance.tooltipUI.SetText("카드키가 필요합니다.");
                }
                foreach (var key in tdscene.PlayerChar.GetComponent<Player>().haveKeycode)
                {
                    if (key == keycode)
                    {
                        if (LockDoorHelpUI.activeSelf && LockDoorHelpUI != null)
                        {
                            LockDoorHelpUI.SetActive(false);
                        }
                        hasexit = false;
                        actived = true;
                        doorOpenSound.Play();
                        isOpen = true;
                        UIManager.Instance.tooltipUI.Hide();
                        break;
                    }
                }
            }
            else
            {
                hasexit = false;
                actived = true;
                if(doorOpenSound != null)
                    doorOpenSound.Play();
            }
        }
    }
    void OnTriggerExit(Collider trig)
    {
        if (trig.GetComponent<Collider>() == tdscene.PlayerChar)
        {
            if (!isOpen)
            {
                actived = false;
                hasexit = true;
            }
        }

    }
}
