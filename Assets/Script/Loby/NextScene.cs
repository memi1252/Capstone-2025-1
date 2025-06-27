using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int SceneNumber;
    
    public void ClickButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneNumber);
        //GameManager.Instance.isCamera = true;
    }
}
