using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    [Serializable]
    public struct QuestData
    {
        public string name;
        public bool clear;
    }
    
    public QuestData[] quests;
    private bool[] questClear;
    private int currentQuestIndex = 0; // 현재 퀘스트 인덱스
    [SerializeField] private float nextQuestDelay = 3f; // 다음 퀘스트 시작 딜레이
    
    [Header("Quest")]
    [SerializeField] private TextMeshProUGUI QuestNameText;
    [SerializeField] private TextMeshProUGUI QuestcountText;

    private void Start()
    {
        questClear = new bool[quests.Length];
        QuestNameText.text = quests[currentQuestIndex].name;
        QuestcountText.text = "0 / 1";
    }

    private void Update()
    {
        if (quests[currentQuestIndex].name != null)
        {
            if (quests[currentQuestIndex].clear && !questClear[currentQuestIndex])
            {
                questClear[currentQuestIndex] = true;
                QuestcountText.text = "1 / 1";
                QuestNameText.text = "퀘스트 완료!";
                StartCoroutine(NextQuest());
            }
        }
    }

    IEnumerator NextQuest()
    {
        yield return new WaitForSeconds(nextQuestDelay);
        currentQuestIndex++;
        if (quests[currentQuestIndex].name != null)
        {
            QuestNameText.text = quests[currentQuestIndex].name;
            QuestcountText.text = "0 / 1";
        }
        
    }
}
