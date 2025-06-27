using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame_Anim : MonoBehaviour
{
    [SerializeField] private Animator animation;
    [SerializeField] private NextScene nextScene;
    private AsyncOperation op;
    void Start()
    {
        // op = SceneManager.LoadSceneAsync(5);
        // op.allowSceneActivation = false;
    }

    void Update()
    {

    }

    public void StartAnim()
    {
        animation.SetTrigger("StartAnim");
        Invoke("NewScene", 5f);
    }

    public void NewScene()
    {
        // op.allowSceneActivation = true;
        nextScene.ClickButton();
    }
}
