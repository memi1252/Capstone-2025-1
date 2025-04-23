using System;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    [Serializable]
    public struct QuestData
    {
        public string type;
        public string name;
        public string content;
    }
    
    public QuestData[] quests;
    
    [Header("Quest")]
    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject questSlot;

    private void Start()
    {
        questUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (questUI.activeSelf)
            {
                questUI.SetActive(false);
            }
            else
            {
                questUI.SetActive(true);
                questSlot.GetComponent<QuestSlot>().SetText(quests[0].type, quests[0].name, quests[0].content);
            }
            
        }
    }
}
