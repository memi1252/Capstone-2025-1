using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplacingpartsDoor : MonoBehaviour
{
    Animator animator;
    private bool one =false;
    public bool Clear = false;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if(GameManager.Instance.player.GetComponent<QuitslotItemSelect>().currentHandItem.name != "mongkeyspanerItem")
            return;
        animator.SetTrigger("Open");
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
        UIManager.Instance.itemDescriptionUI.SetActive(false);
        GameManager.Instance.playerCamera.gameObject.SetActive(false);
        GameManager.Instance.miniGameScene = false;
    }
}
