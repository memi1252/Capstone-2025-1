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
        SceneManager.LoadScene(0);
    }
}
