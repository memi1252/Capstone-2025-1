using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;

public class Loding : MonoBehaviour
{
    [SerializeField] private Slider Gauge;
    [SerializeField] private int SceneNumber;

    public float currentTime;
    public float currentBBASSMove;
    private AsyncOperation op;
    void Start()
    {
        StartCoroutine(LodingGameScene());
    }

    IEnumerator LodingGameScene()
    {
        op = SceneManager.LoadSceneAsync(SceneNumber);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (Gauge.value < 0.8f)
            {
                Gauge.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                Gauge.value = Mathf.Lerp(0.9f, 1f, timer);
                if (Gauge.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
