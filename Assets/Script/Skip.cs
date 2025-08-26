using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skip : MonoBehaviour
{
    [SerializeField] private Image skipImage;
    [SerializeField] private float maxSkipTime = 5f;
    private float currentSkipTime;

    private void Update()
    {
        skipImage.transform.position = Input.mousePosition;

        
            if (Input.GetMouseButton(0))
            {
                skipImage.gameObject.SetActive(true);
                currentSkipTime += Time.deltaTime;
                skipImage.fillAmount = currentSkipTime / maxSkipTime;
                if (currentSkipTime >= maxSkipTime)
                {
                    GameManager.Instance.MouseCursor(true);
                    SceneManager.LoadScene(0);
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
