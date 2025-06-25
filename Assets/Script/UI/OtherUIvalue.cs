using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OtherUIvalue : MonoBehaviour
{
    public Slider fatigueSlider;
    public Slider HPSlider;
    void Start()
    {
    }

    void Update()
    {
        fatigueSlider.GetComponent<Slider>().value -= Time.deltaTime * 1;
    }
    
}
