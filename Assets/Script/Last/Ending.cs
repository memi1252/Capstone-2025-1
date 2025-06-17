using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour
{
    public GameObject cut1Camera;
    public GameObject cut2Camera;
    public GameObject cut3Camera;
    void Start()
    {
        StartCoroutine(Cut1());
    }

    void Update()
    {

    }

    IEnumerator Cut1()
    {
        yield return new WaitForSeconds(8f);
        cut2Camera.SetActive(true);
        StartCoroutine(Cut2());

    }
    IEnumerator Cut2()
    {
        cut1Camera.SetActive(false);
        yield return new WaitForSeconds(3f);
    }
    
}
