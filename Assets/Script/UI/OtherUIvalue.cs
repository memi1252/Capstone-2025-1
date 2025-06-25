using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OtherUIvalue : MonoBehaviour
{
    public Slider fatigueSlider;
    public Slider HPSlider;
    public Slider Oxy1;
    public Slider Oxy2;
    public float value;
    public float maxOxy;
    public float currentOxy1;
    public float currentOxy2;
    void Start()
    {
        currentOxy1 = maxOxy;
        currentOxy2 = maxOxy ;
    }

    void Update()
    {
        Oxy2.value = currentOxy2/ maxOxy;
        Oxy1.value = currentOxy1 / maxOxy;
        fatigueSlider.value -= Time.deltaTime * 0.4f;
        if (fatigueSlider.value <= 0)
        {
            HPSlider.value -= Time.deltaTime * 3;
        }

        if (!GameManager.Instance.inSpaceShip)
        {
            if (currentOxy2 >= 0)
            {
                currentOxy2 -= Time.deltaTime * value;
            }
            else
            {
                currentOxy1 -= Time.deltaTime * value;
            }
        }
    }
    
}
