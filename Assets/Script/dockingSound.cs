using UnityEngine;

public class dockingSound : MonoBehaviour
{
    public AudioSource dockingBackgroundSound;
    public GameObject help;
    void Start()
    {
        
    }

    void Update()
    {
        if (help.activeSelf == false)
        {
            dockingBackgroundSound.Play();
        }
    }
}
