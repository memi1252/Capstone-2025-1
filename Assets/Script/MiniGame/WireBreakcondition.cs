using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireBreakcondition : MonoBehaviour
{
    [SerializeField] private Wire[] wires;
    [SerializeField] private Wire[] breakWire;
    [SerializeField] private int cutCount;

    private bool Success = false;
    private bool Fail = false;
    

    private void Update()
    {
        foreach (var wire in breakWire)
        {
            if(wire.isCut)
            {
                wire.gameObject.SetActive(false);

                if (!wire.isCount)
                {
                    wire.isCount = true;
                    cutCount++;
                }
            }
        }
        
        
        if(cutCount == breakWire.Length)
        {
            Success = true;
            WireManager.instance.Success();
            StartCoroutine(RetrunScene());
        }
        

        if (!Fail)
        {
            foreach (var wire in wires)
            {
                if (wire.isCut)
                {
                    wire.gameObject.SetActive(false);
                    Fail = true;
                    StartCoroutine(RetrunScene());
                    WireManager.instance.Fail();
                    break;
                }
            }
        }
        
    }
    
    IEnumerator RetrunScene()
    {
        yield return new WaitForSeconds(2f);
        WireManager.instance.statusText.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync(GameManager.Instance.WireConnectionScene);
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        GameManager.Instance.miniGameScene = true;
        GameManager.Instance.playerCamera.gameObject.SetActive(true);
        UIManager.Instance.StastUI.SetActive(true);
        UIManager.Instance.MinMapUI.SetActive(true);
        UIManager.Instance.QuitSlotUI.SetActive(true);
        UIManager.Instance.QuestUI.SetActive(true);
    }

    
}
