using UnityEngine;
using System.Collections;

public class WireRule : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
    void Start()
    {
        StartCoroutine(HelpBook());
    }

    void Update()
    {

    }
    IEnumerator HelpBook()
    {
        yield return new WaitForSeconds(4f);
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void HelpBookOut()
    {
        WireManager.instance.TurnOnLights();
        WireManager.instance.start = true;
        page2.SetActive(false);
    }
}
