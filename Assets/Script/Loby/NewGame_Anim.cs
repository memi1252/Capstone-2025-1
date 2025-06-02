using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame_Anim : MonoBehaviour
{
    [SerializeField] private Animator animation;
    void Start()
    {

    }

    void Update()
    {

    }

    public void StartAnim()
    {
        animation.SetTrigger("StartAnim");
        Invoke("NewScene",5f);
    }

    public void NewScene()
    {
        SceneManager.LoadScene(2);
    }
}
