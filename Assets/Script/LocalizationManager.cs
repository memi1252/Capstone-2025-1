// LocalizationManager.cs

using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions; // 정규식 포함

// 다른 스크립트에서 먼저 실행되도록 순서 조정
[DefaultExecutionOrder(-200)]
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    private Dictionary<string, Dictionary<string, string>> localizationData;
    private List<string> languageCodes;
    public string CurrentLanguage { get; set; } = "en"; // 기본 언어 설정
    public event Action OnLanguageChanged;

    // CSV 파싱을 위한 정규 표현식 (따옴표 내부 쉼표 무시)
    private static readonly Regex CSV_SPLIT_REGEX = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

    void Awake()
    {
        // 싱글톤 처리
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); // 데이터 로드
        }
        else
        {
            Destroy(gameObject);
        }

        if(PlayerPrefs.HasKey("Language"))
        {
            CurrentLanguage = PlayerPrefs.GetString("Language");
        }
    }

    private void Update()
    {
        
    }

    private void LoadData()
    {
        localizationData = new Dictionary<string, Dictionary<string, string>>();
        languageCodes = new List<string>();

        TextAsset csvFile = Resources.Load<TextAsset>("Localization");
        if (csvFile == null)
        {
            Debug.LogError("Resources/Localization.csv 파일을 찾을 수 없습니다!");
            return;
        }

        using (StringReader reader = new StringReader(csvFile.text))
        {
            // 헤더 읽기
            string headerLine = reader.ReadLine();
            if (headerLine == null) return;
            string[] headers = CSV_SPLIT_REGEX.Split(headerLine);

            for (int i = 1; i < headers.Length; i++)
            {
                languageCodes.Add(headers[i].Trim());
            }

            // 데이터 읽기
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = CSV_SPLIT_REGEX.Split(line);
                if (values.Length < headers.Length) continue;

                string key = values[0].Trim();
                if (string.IsNullOrEmpty(key)) continue;

                if (!localizationData.ContainsKey(key))
                {
                    localizationData[key] = new Dictionary<string, string>();
                }

                for (int i = 1; i < headers.Length; i++)
                {
                    string langCode = languageCodes[i - 1];
                    string value = values[i].Trim();
                    // 따옴표 제거
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }
                    value = value.Replace("\"\"", "\"");
                    localizationData[key][langCode] = value;
                }
            }
        }
        Debug.Log("지역화 데이터 로드 완료.");
    }

    // 텍스트를 요청하는 공개 함수
    public string GetText(string key)
    {
        if (localizationData != null && localizationData.ContainsKey(key))
        {
            if (localizationData[key].ContainsKey(CurrentLanguage))
            {
                return localizationData[key][CurrentLanguage];
            }
        }
        Debug.LogWarning($"지역화 키를 찾을 수 없습니다: [Key: {key}, Lang: {CurrentLanguage}]");
        return $"#{key}#"; // 키를 못 찾으면 키 자체를 반환
    }

    public void RefreshAllText()
    {
        // 현재 활성화된 모든 LocalizedText 오브젝트를 찾습니다.
        LocalizedText[] allTextObjects = FindObjectsOfType<LocalizedText>();

        foreach (LocalizedText textObject in allTextObjects)
        {
            textObject.UpdateText(); // 각 텍스트 오브젝트의 UpdateText() 함수를 호출
        }
    }
}