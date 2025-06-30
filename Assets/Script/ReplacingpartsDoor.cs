using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplacingpartsDoor : MonoBehaviour
{
    Animator animator;
    private bool one =false;
    public bool Clear = false;
    public GameObject[] partials;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if(GameManager.Instance.player.GetComponent<QuitslotItemSelect>().currentHandItem.name != "mongkeyspanerItem")
            return;
        animator.SetTrigger("Open");
        foreach (GameObject part in partials)
        {
            if (part != null)
            {
                part.SetActive(true);
            }
        }
        GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        if (!one)
        {
            StartCoroutine(Scemeload());
            one = true;
        }
    }

    public void Close()
    {
        one = false;
        animator.SetTrigger("Close");
        if (Clear)
        {
            foreach (GameObject part in partials)
            {
                if (part != null)
                {
                    part.SetActive(false);
                }
            }
        }
    }

    IEnumerator Scemeload()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ReplacingpartsScene", LoadSceneMode.Additive);
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
        GameManager.Instance.MouseCursor(true);
        UIManager.Instance.StastUI.SetActive(false);
        UIManager.Instance.QuitSlotUI.SetActive(false);
        UIManager.Instance.BBASSViewUI.SetActive(false);
        GameManager.Instance.playerCamera.gameObject.SetActive(false);
        GameManager.Instance.miniGameScene = false;
    }
}
