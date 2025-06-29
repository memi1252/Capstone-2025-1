using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : UIBase
{
    [SerializeField] private TextMeshProUGUI DesText;
    [SerializeField] private Button CancelButton;
    [SerializeField] private Button UseButton;

    private item itemData;
    private void Start()
    {
        CancelButton.onClick.AddListener(() =>
        {
            itemData.Backd();
        });
        UseButton.onClick.AddListener(() =>
        {
            itemData.Pickup();
        });
        Hide();
    }

    public void SetItem(item item)
    {
        itemData = item;
        DesText.text = itemData.itemDescription;
    }
    
    
}
