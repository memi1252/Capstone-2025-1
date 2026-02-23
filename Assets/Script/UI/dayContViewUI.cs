using TMPro;
using UnityEngine;

public class dayContViewUI : MonoBehaviour
{
    private Animator animator;
    private TextMeshProUGUI Text;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void DayCountPlay(int count)
    {
        if (animator != null)
        {
            animator.SetTrigger("Play");
            Text.text = $"--{LocalizationManager.Instance.GetText("day1")}{count}{LocalizationManager.Instance.GetText("day2")}--";
        }
    }
}
