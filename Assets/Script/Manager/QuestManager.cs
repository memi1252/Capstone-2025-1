using System;
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
    
    [Header("Quest")]
    [SerializeField] private TextMeshProUGUI QuestNameText;
    [SerializeField] private TextMeshProUGUI QuestcountText;

    private void Start()
    {
        QuestNameText.text = quests[0].name;
        QuestcountText.text = "0 / 1";
    }

    private void Update()
    {
        if (!quests[0].clear)
        {
            
        }
        
    }
}
