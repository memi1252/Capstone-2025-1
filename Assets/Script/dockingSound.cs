using UnityEngine;

public class dockingSound : MonoBehaviour
{
    public AudioSource _dockingSound;
    public AudioClip dockingClear;
    public AudioClip dockingClear2;
    public GameObject help;
    void Start()
    {

    }

    public void docking_B_Player()
    {
        if (help.activeSelf == false)
        {
            _dockingSound.Play();
        }
    }

    public void docking_Clear()
    {
        _dockingSound.Stop();
        _dockingSound.PlayOneShot(dockingClear);
        Invoke("docking_Clear2", 2f);
    }
    void docking_Clear2()
    {
        _dockingSound.Stop();
        _dockingSound.PlayOneShot(dockingClear2);
    }
}
