using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine.Video;

public class Loding : MonoBehaviour
{
    [SerializeField] private Slider Gauge;
    [SerializeField] private int SceneNumber;
    [SerializeField] private VideoPlayer VideoPlayer;
    [SerializeField] private GameObject LongText;
    [SerializeField] private GameObject skipTrueText;
    [SerializeField] private float LongMoveSpeed = 0.5f;
    [SerializeField] private float nextSceneTime = 5f;
    [SerializeField] private Image skipImage;
    [SerializeField] private float maxSkipTime = 5f;
    
    private float currentSkipTime;
    
    
    private AsyncOperation op;
    private bool skip = false;
    void Start()
    {
        VideoPlayer.Play();
        StartCoroutine(LodingGameScene());
        StartCoroutine(LodingGame());
    }

    IEnumerator LodingGameScene()
    {
        op = SceneManager.LoadSceneAsync(SceneNumber);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress >= 0.9f)
            {
                skip = true;
                skipTrueText.SetActive(true);
            }
            yield break;
        }
    }
    
    private void Update()
    {
        skipImage.transform.position = Input.mousePosition;
        
        if (!VideoPlayer.isPlaying)
        {
            LongText.transform.Translate(Vector2.up * Time.deltaTime * LongMoveSpeed); //이어 설명 나오는부분
        }

        if (skip)
        {
            if (Input.GetMouseButton(0))
            {
                skipImage.gameObject.SetActive(true);
                currentSkipTime += Time.deltaTime;
                skipImage.fillAmount = currentSkipTime / maxSkipTime;
                if (currentSkipTime >= maxSkipTime)
                {
                    op.allowSceneActivation = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentSkipTime = 0;
                skipImage.fillAmount = 0;
                skipImage.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LodingGame()
    {
        yield return new WaitForSeconds(nextSceneTime);
        op.allowSceneActivation = true;
    }
}
