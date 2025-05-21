using System;
using UnityEngine;

public class RotaionGame : MonoBehaviour
{
    [SerializeField] private ScrewPoint[] screwPoints;

    private void Update()
    {
        for (int i = 0; i <  screwPoints.Length; i++)
        {
            if (!screwPoints[i].Seccess)
            {
                break;
            }
            if (i ==  screwPoints.Length - 1)
            {
                Hide();
            }
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GameManager.Instance.ismove = false;
        GameManager.Instance.isCamera = false;
        RenderSettings.ambientIntensity = 5;
        GameManager.Instance.MouseCursor(true);
    }

    public void Hide()
    {
        RenderSettings.ambientIntensity = 0;
        GameManager.Instance.ismove = true;
        GameManager.Instance.isCamera = true;
        GameManager.Instance.MouseCursor(false);
        gameObject.SetActive(false);
    }
}
