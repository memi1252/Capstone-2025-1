using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
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

    public bool isOpen;
    public string keycode;
    public bool hasValidKey = false;

    public AudioSource doorOpenSound;
    public AudioSource cardKeyDoorSound;

    private void Start()
    {
        tdscene = GameObject.FindWithTag("TdLevelManager").GetComponent<TDScene>();
    }
    void OnTriggerEnter(Collider trig)
    {
        if (trig.GetComponent<Collider>() == tdscene.PlayerChar)
        {
            if (keyActive)
            {
                

                if (tdscene.PlayerChar.GetComponent<Player>().haveKeycode == null)
                {
                    cardKeyDoorSound.Play();
                    hasValidKey = false;
                }
                else
                {
                    foreach (var key in tdscene.PlayerChar.GetComponent<Player>().haveKeycode)
                    {
                        if (key == keycode)
                        {
                            hasValidKey = true;
                            break;
                        }
                    }

                    if (!hasValidKey)
                    {
                        cardKeyDoorSound.Play();
                        UIManager.Instance.tooltipUI.SetText("카드키가 필요합니다.");
                    }
                }

                if (hasValidKey)
                {
                    if (LockDoorHelpUI != null)
                    {
                        LockDoorHelpUI.SetActive(false);
                    }

                    // 기존 문 열기 로직
                    hasexit = false;
                    actived = true;
                    if (doorOpenSound != null)
                    {
                        doorOpenSound.Stop(); // 이전 재생 중인 소리를 중지
                        doorOpenSound.Play(); // 소리 재생
                    }
                    isOpen = true;
                    UIManager.Instance.tooltipUI.Hide();
                }
            }
            else
            {
                hasexit = false;
                actived = true;
                if (doorOpenSound != null)
                {
                    doorOpenSound.Stop(); // 이전 재생 중인 소리를 중지
                    doorOpenSound.Play(); // 소리 재생
                }
            }
        }
    }

    void OnTriggerStay(Collider trig)
    {
        if (trig.GetComponent<Collider>() == tdscene.PlayerChar)
        {
            if (keyActive)
            {
                

                if (tdscene.PlayerChar.GetComponent<Player>().haveKeycode == null)
                {
                    hasValidKey = false;
                }
                else
                {
                    foreach (var key in tdscene.PlayerChar.GetComponent<Player>().haveKeycode)
                    {
                        if (key == keycode)
                        {
                            hasValidKey = true;
                            break;
                        }
                    }

                    if (!hasValidKey)
                    {
                        UIManager.Instance.tooltipUI.SetText("카드키가 필요합니다.");
                    }
                }

                if (hasValidKey)
                {
                    if (LockDoorHelpUI != null)
                    {
                        LockDoorHelpUI.SetActive(false);
                    }

                    if (GameManager.Instance.CardKetDoor1 && !QuestManager.Instance.quests[10].clear)
                    {
                        QuestManager.Instance.quests[10].clear = true;
                        GameManager.Instance.CardKetDoor1 = false;
                    }
                    
                    if (GameManager.Instance.CardKetDoor2 && !QuestManager.Instance.quests[18].clear)
                    {
                        QuestManager.Instance.quests[18].clear = true;
                        GameManager.Instance.CardKetDoor2 = false;
                    }

                    // 기존 문 열기 로직
                    hasexit = false;
                    actived = true;
                   
                    isOpen = true;
                    UIManager.Instance.tooltipUI.Hide();
                }
            }
            else
            {
                hasexit = false;
                actived = true;
                
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
                UIManager.Instance.tooltipUI.Hide();
            }
        }

    }
}
