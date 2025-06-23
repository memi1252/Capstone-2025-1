using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using TMPro;
public class SaveDatas
{
    public Vector3 playerPosition;
    public string fileName;
}
public class DataManager : MonoBehaviour
{
    public SaveDatas[] _saveData = new SaveDatas[3];
    string[] jsonData = { null, null, null };

    public TextMeshProUGUI _fileName;
    public TextMeshProUGUI[] gameFileName;

    string json;
    string path;

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            string path = Application.persistentDataPath + $"/save_{i}.json";
            if (File.Exists(path))
            {
                string loadedJson = File.ReadAllText(path);
                _saveData[i] = JsonUtility.FromJson<SaveDatas>(loadedJson);
                jsonData[i] = loadedJson;
            }
        }
        UpdateSlotFileNames();
    }

    public void UpdateSlotFileNames()
    {
        for (int i = 0; i < gameFileName.Length; i++)
        {
            path = Application.persistentDataPath + $"/save_{i}.json";

            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
                SaveDatas temp = JsonUtility.FromJson<SaveDatas>(json);

                if (temp != null && !string.IsNullOrEmpty(temp.fileName))
                    gameFileName[i].text = temp.fileName;
                else
                    gameFileName[i].text = "빈 슬롯";
            }
            else
            {
                gameFileName[i].text = "빈 슬롯";
            }
        }
    }

    int fileNumber;
    public void GameSave()
    {
        _saveData[fileNumber] = new SaveDatas()
        {
            fileName = _fileName.text,
        };

        jsonData[fileNumber] = JsonUtility.ToJson(_saveData[fileNumber]);
        path = Application.persistentDataPath + $"/save_{fileNumber}.json";
        File.WriteAllText(path, jsonData[fileNumber]);
        Debug.Log($"슬롯 {fileNumber} 저장 완료 at {path}");
    }

    public void Gameload()
    {
        path = Application.persistentDataPath + $"/save_{fileNumber}.json";
        if (File.Exists(path))
        {
            string loadedJson = File.ReadAllText(path);
            _saveData[fileNumber] = JsonUtility.FromJson<SaveDatas>(loadedJson);
            Debug.Log($"로드 완료: {_saveData[fileNumber].fileName}");
        }
        else
        {
            fileNameInputField.SetActive(true);
            Debug.LogWarning("저장된 파일이 없습니다.");
        }
    }
    public GameObject fileNameInputField;
    public void cheak(int num)
    {

        fileNumber = num;
        Debug.Log("num = " + num);
        Debug.Log("filenumber = " + fileNumber);
        Gameload();

    }

    public void nameClear()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            File.Delete(json);
        }
    }
}
