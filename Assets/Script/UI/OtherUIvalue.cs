using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OtherUIvalue : MonoBehaviour
{
    public Slider fatigueSlider;
    public Slider HPSlider;
    public Slider Oxy1;
    public Slider Oxy2;
    void Start()
    {
    }

    void Update()
    {
        fatigueSlider.GetComponent<Slider>().value -= Time.deltaTime * 0.4f;
        if (fatigueSlider.GetComponent<Slider>().value <= 0)
        {
            HPSlider.GetComponent<Slider>().value -= Time.deltaTime * 3;
        }

        if (GameManager.Instance.inSpaceShip == false)
        {
            if (Oxy1.GetComponent<Slider>().value > 0)
            {
                Oxy1.GetComponent<Slider>().value -= Time.deltaTime * 0.4f;
            }
            else
            {
                Oxy2.GetComponent<Slider>().value -= Time.deltaTime * 0.4f;
            }
        }
    }
    
}
