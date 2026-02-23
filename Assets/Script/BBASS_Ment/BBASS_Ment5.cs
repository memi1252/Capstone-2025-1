using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBASS_Ment5 : BBASS_MentBASE
{
    public bool play = false;
    public int LoadSceneNumber;

    private void Update()
    {
        if (play)
        {
            UIManager.Instance.tooltipUI.Hide();
        }
    }

    public override IEnumerator PrintDialogList(List<DialogData> dataList)
    {
        play = true;
        UIManager.Instance.combination2.SetActive(false);
        UIManager.Instance.combination3.SetActive(true);
        yield return StartCoroutine(base.PrintDialogList(dataList));
        
        play = false;
        Printer.SetActive(false);
        
        // Quest 상태 업데이트를 먼저 수행 (GameObjects 파괴 전에)
        if (QuestManager.Instance != null && QuestManager.Instance.quests != null && QuestManager.Instance.quests.Length > 21)
        {
            QuestManager.Instance.quests[21].clear = true;
        }
        //FindAnyObjectByType<SpaceDoorOpen>().isOpen = true;
        
        // AsyncOperation null 체크 및 씬 전환 처리
        if (GameManager.Instance != null && GameManager.Instance.finerAsync != null)
        {
            GameManager.Instance.finerAsync.allowSceneActivation = true;
        }
        else
        {
            // finerAsync가 null인 경우 직접 씬 로드
            Debug.LogWarning("finerAsync is null, loading scene directly");
            // 코루틴으로 안전하게 씬 전환
            StartCoroutine(LoadSceneSafely());
        }
        
        // GameObjects 파괴 (씬 전환이 시작된 후)
        StartCoroutine(DestroyManagersSafely());
        
        enabled = false;
    }
    
    private IEnumerator LoadSceneSafely()
    {
        yield return new WaitForSeconds(0.1f); // 프레임 대기
        SceneManager.LoadScene(6);
    }
    
    private IEnumerator DestroyManagersSafely()
    {
        yield return new WaitForSeconds(0.2f); // 씬 전환이 시작될 때까지 대기
        
        // 안전하게 각 매니저 파괴
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        
        if (UIManager.Instance != null)
        {
            Destroy(UIManager.Instance.gameObject);
        }
        
        if (QuestManager.Instance != null)
        {
            Destroy(QuestManager.Instance.gameObject);
        }
    }
    

    public void line()
    {
        QuestManager.Instance.quests[22].clear = true;
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story65")));
        dialogTexts.Add(new DialogData(LocalizationManager.Instance.GetText("Story66")));
       
        Show(dialogTexts);
    }
}
