using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OtherUIvalue : MonoBehaviour
{
    public Slider fatigueSlider;
    public Slider HPSlider;
    void Start()
    {
        StartCoroutine(FatigueBar());
        // StartCoroutine(HPbar());
    }

    void Update()
    {
        
    }
    IEnumerator FatigueBar()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f);
            Gague();
        }
    }

    // IEnumerator HPbar()
    // {
    //     while (true)
    //     {
    //         if (fatigueSlider.GetComponent<Slider>().value <= 90)
    //         {
    //             yield return new WaitForSeconds(7f);
    //             HPSlider.GetComponent<Slider>().value -= 5;
    //         }
    //     }
    // }

    void Gague()   
    {
        fatigueSlider.GetComponent<Slider>().value -= 7;
    }
}
