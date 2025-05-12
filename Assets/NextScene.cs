using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int SceneNumber;
    
    public void ClickButton()
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
