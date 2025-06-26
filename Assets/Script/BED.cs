using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BED : MonoBehaviour
{
    public Image fadeImage; // 화면 페이드 아웃을 위한 이미지
    public float fadeDuration = 1f; // 페이드 아웃 시간
    public float sleepRecoveryTime = 5f; // 수면으로 인한 회복 시간

    public int DayCount = 1; // 현재 날짜 카운트
    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0); // 초기 색상은 투명
        }
    }

    public void GoToSleep()
    {
        StartCoroutine(SleepCoroutine());
    }

    private IEnumerator SleepCoroutine()
    {
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

        UIManager.Instance.dayContViewUI.DayCountPlay(DayCount);
    }

    private void RecoverFatigueAndHealth()
    {
        // 피로도와 HP 회복 로직 구현
        OtherUIvalue otherUIValue = UIManager.Instance.StastUI.GetComponent<OtherUIvalue>();
        otherUIValue.currentHp = otherUIValue.MaxHp;
        otherUIValue.currentFatigue = otherUIValue.maxFatigue;
    }
    public void Sleep()
    {
       //화면이 꺼해지는 연출
       
       
       // 피로도 회복 , hp회복
       
       //화면이 밣아 지면서 날자뜸
    }
}
