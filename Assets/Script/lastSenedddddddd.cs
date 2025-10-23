using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lastSenedddddddd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ss());
    }

    IEnumerator ss()
    {
        yield return new WaitForSeconds(190);
        if(GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        if (UIManager.Instance != null)
        {
            Destroy(UIManager.Instance.gameObject);
        }

        if (QuestManager.Instance != null)
        {
            Destroy(QuestManager.Instance.gameObject);
        }
        SceneManager.LoadScene(0);
    }
}
