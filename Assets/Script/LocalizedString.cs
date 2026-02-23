using UnityEngine;

public class LocalizedString : MonoBehaviour
{
    [Header("지역화 키")]
    public string textKey;
    
    // void Start()
    // {
    //     textComponent = GetComponent<TextMeshProUGUI>();
    //     UpdateText();
    // }
    //
    // // 텍스트를 업데이트하는 함수
    // public void UpdateText()
    // {
    //     if (textComponent == null)
    //     {
    //         textComponent = GetComponent<TextMeshProUGUI>();
    //     }
    //
    //     if (LocalizationManager.Instance != null && !string.IsNullOrEmpty(textKey))
    //     {
    //         textComponent.text = LocalizationManager.Instance.GetText(textKey);
    //     }
    //     else
    //     {
    //         textComponent.text = $"#{textKey}#";
    //     }
    // }
    //
    // // (에디터 전용) Inspector에서 키 값을 바꾸면 에디터에서 바로 보이게 함
    // void OnValidate()
    // {
    //     if (textComponent == null)
    //     {
    //         textComponent = GetComponent<TextMeshProUGUI>();
    //     }
    //
    //     // LocalizationManager가 씬에 없을 경우 오류 방지 처리
    //     try
    //     {
    //         if (LocalizationManager.Instance != null && !string.IsNullOrEmpty(textKey))
    //         {
    //             textComponent.text = 
    //         }
    //         else
    //         {
    //             textComponent.text = $"#{textKey}#";
    //         }
    //     }
    //     catch (System.Exception)
    //     {
    //         // 게임 실행 전에는 LocalizationManager가 없을 수 있으므로 Preview 텍스트 표시
    //         if(textComponent != null)
    //         {
    //             textComponent.text = $"#{textKey}# (Preview)";
    //         }
    //     }
    // }
}
