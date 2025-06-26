using UnityEngine;

public class dockingSound : MonoBehaviour
{
    public AudioSource _dockingSound;
    public AudioClip dockingBackground;
    public AudioClip dockingClear;
    public GameObject help;
    void Start()
    {

    }

    public void docking_B_Player()
    {
        if (help.activeSelf == false)
        {
            _dockingSound.PlayOneShot(dockingBackground);
        }
    }

    public void docking_Clear()
    {
        _dockingSound.PlayOneShot(dockingBackground);
    }
}
