// Assets/Editor/LocalizationDataDownloader.cs
using UnityEngine;
using UnityEditor;
using System.Net.Http;
using System.IO;

[CreateAssetMenu(fileName = "LocalizationDownloader", menuName = "class/Localization Downloader")]
public class LocalizationDataDownloader : ScriptableObject
{
    [Header("구글 시트 '공개된' CSV 게시 URL")]
    [TextArea(3, 5)]
    public string sheetURL;

    // Inspector에 오른쪽 메뉴(점 3개)에서 이 함수를 실행
    [ContextMenu("Download Localization Data")]
    public async void DownloadData()
    {
        if (string.IsNullOrEmpty(sheetURL))
        {
            Debug.LogError("구글 시트 URL이 비어있습니다!");
            return;
        }

        Debug.Log("구글 시트 데이터 다운로드 시작...");

        try
        {
            using (HttpClient client = new HttpClient())
            {
                string csvData = await client.GetStringAsync(sheetURL);
                if (string.IsNullOrEmpty(csvData))
                {
                    Debug.LogError("데이터를 다운로드하지 못했거나 시트가 비어있습니다.");
                    return;
                }

                // 1. Resources 폴더 경로 확인
                string resourcesPath = Path.Combine(Application.dataPath, "Resources");
                if (!Directory.Exists(resourcesPath))
                {
                    Directory.CreateDirectory(resourcesPath);
                }

                // 2. CSV 파일 저장
                string filePath = Path.Combine(resourcesPath, "Localization.csv");
                File.WriteAllText(filePath, csvData);

                // 3. Unity 에디터에 반영
                AssetDatabase.Refresh();
                Debug.Log("<color=green>다운로드 완료! 'Assets/Resources/Localization.csv'에 저장되었습니다.</color>");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"데이터 다운로드 실패: {e.Message}");
        }
    }
}