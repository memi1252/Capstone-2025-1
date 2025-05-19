using TMPro;
using UnityEngine;

public class QuestSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestTypeText;
    [SerializeField] private TextMeshProUGUI QuestNameText;
    [SerializeField] private TextMeshProUGUI QuestContentText;

    public void SetText(string type, string name, string content)
    {
        QuestTypeText.text = type;
        QuestNameText.text = name;
        QuestContentText.text = content;
    }
}
