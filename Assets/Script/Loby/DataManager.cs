using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
public class saveData
{
    public Vector3 playerPosition;
}
public class DataManager : MonoBehaviour
{
    public saveData SaveData = new saveData();
    string jsonData;

    void Awake()
    {

    }

    public void GameSave()
    {
        jsonData = JsonUtility.ToJson(SaveData);
    }
    public void Gameload()
    {

    }
    public GameObject fileName;
    public void cheak()
    {
        if (jsonData == null)
        {
            fileName.SetActive(true);
        }
    }

    public void nameClear()
    {
        
    }
}
