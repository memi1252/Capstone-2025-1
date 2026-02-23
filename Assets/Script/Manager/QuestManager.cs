using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    [Serializable]
    public struct QuestData
    {
        public string name;
        public bool clear;
        public Transform[] pos;
        public doorIndex[] doors;
    }
    
    [Serializable]
    public struct doorIndex
    {
        public int index;
        public bool open;
    }
    
    public QuestData[] quests;
    private int index = 0;
    public int doorindex = 0;
    private bool[] questClear;
    public int currentQuestIndex = 0; // 현재 퀘스트 인덱스
    [SerializeField] private float nextQuestDelay = 3f; // 다음 퀘스트 시작 딜레이
    
    [Header("Quest")]
    [SerializeField] private TextMeshProUGUI QuestNameText;
    [SerializeField] private TextMeshProUGUI QuestcountText;

    private void Start()
    {
        questClear = new bool[quests.Length];
        QuestNameText.text = LocalizationManager.Instance.GetText(quests[currentQuestIndex].name);
        QuestcountText.text = "0 / 1";
    }

    public void dd()
    {
        index++;
    }

    private void Update()
    {
        if (currentQuestIndex < quests.Length)
        {

            if (quests[currentQuestIndex].clear && !questClear[currentQuestIndex])
            {
                questClear[currentQuestIndex] = true;
                QuestcountText.text = "1 / 1";
                QuestNameText.text = LocalizationManager.Instance.GetText("quest0");
                StartCoroutine(NextQuest());
            }
            if (quests[currentQuestIndex].pos[index] != null)
            {
                WayPointUI.Instance.target = quests[currentQuestIndex].pos[index];
            }
        }
    }
    
    public void ResetQuestIndex()
    {
        quests[currentQuestIndex].pos = null;
    }

    IEnumerator NextQuest()
    {
        yield return new WaitForSeconds(nextQuestDelay);
        currentQuestIndex++;
        index = 0;
        doorindex = 0;
        if (currentQuestIndex != quests.Length)
        {
            QuestNameText.text = LocalizationManager.Instance.GetText(quests[currentQuestIndex].name);
            QuestcountText.text = "0 / 1";
        }

    }
}



