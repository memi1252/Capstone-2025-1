using UnityEngine;

public class RotaionGame : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
        RenderSettings.ambientIntensity = 5;
        GameManager.Instance.MouseCursor(false);
    }

    public void Hide()
    {
        RenderSettings.ambientIntensity = 0;
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(true);
        gameObject.SetActive(false);
    }
}
