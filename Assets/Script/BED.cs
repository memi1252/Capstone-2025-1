using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;
using UnityEngine.UI;

public class BED : MonoBehaviour
{
    public Image fadeImage; // 화면 페이드 아웃을 위한 이미지
    public float fadeDuration = 1f; // 페이드 아웃 시간
    public float sleepRecoveryTime = 5f; // 수면으로 인한 회복 시간

    public int DayCount = 1; // 현재 날짜 카운트
    
    private bool getUp1 = false; 
    private bool getUp2 = false;
    
    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0); // 초기 색상은 투명
            fadeImage.gameObject.SetActive(false);
        }
    }

    public void GoToSleep()
    {
        StartCoroutine(SleepCoroutine());
    }

    private IEnumerator SleepCoroutine()
    {
        fadeImage.gameObject.SetActive(true);
        // 화면 페이드 아웃
        if (fadeImage != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        GameManager.Instance.isCamera = false; 
        GameManager.Instance.ismove = false; 
        GameManager.Instance.noInventoryOpen = true; 
        GameManager.Instance.noESC = true;
        foreach (var doorOpen in FindObjectsByType<SpaceDoorOpen>((FindObjectsSortMode)FindObjectsInactive.Include))
        {
            doorOpen.isOpen = false;
        }
        if (!getUp1)
        {
            GameManager.Instance.BBASS.GetComponent<BBASS_Ment2>().enabled = false;
            GameManager.Instance.BBASS.GetComponent<BBASS_Ment3>().enabled = true;
            QuestManager.Instance.quests[6].clear = true;
            getUp1 = true;
        }
        else
        {
            if (!getUp1)
            {
                //GameManager.Instance.BBASS.GetComponent<BBASS_Ment4>().enabled = false;
                getUp2 = true;
            }
            else
            {
                //GameManager.Instance.BBASS.GetComponent<BBASS_Ment5>().enabled = false;
            }
        }

        DayCount++;
        // 피로도 회복 및 hp 회복 로직
        RecoverFatigueAndHealth();

        // 화면이 밝아지면서 날짜 표시
        yield return new WaitForSeconds(sleepRecoveryTime);
        
        if (fadeImage != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        fadeImage.gameObject.SetActive(false);
        GameManager.Instance.isCamera = true; 
        GameManager.Instance.ismove = true;
        GameManager.Instance.noInventoryOpen = false;
        GameManager.Instance.noESC = false;

        UIManager.Instance.dayContViewUI.DayCountPlay(DayCount);
    }

    private void RecoverFatigueAndHealth()
    {
        // 피로도와 HP 회복 로직 구현
        OtherUIvalue otherUIValue = UIManager.Instance.StastUI.GetComponent<OtherUIvalue>();
        otherUIValue.currentHp = otherUIValue.MaxHp;
        otherUIValue.currentFatigue = otherUIValue.maxFatigue;
    }
    
    
    
}
