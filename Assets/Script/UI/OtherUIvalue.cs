using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OtherUIvalue : MonoBehaviour
{
    public Slider fatigueSlider;
    public Slider HPSlider;
    public Slider Oxy1;
    public Slider Oxy2;
    public float OxyIncrease;
    public float maxOxy;
    public float currentOxy1;
    public float currentOxy2;
    public float HpIncrease =3;
    public float MaxHp;
    public float currentHp;
    public float fatIncrease = 0.4f;
    public float maxFatigue = 100f;
    public float currentFatigue;
    public bool isFat =false;
    public float dayIncrease = 30f;
    void Start()
    {
        currentOxy1 = maxOxy;
        currentOxy2 = maxOxy ;
        currentHp = MaxHp;
        currentFatigue = maxFatigue;
    }

    void Update()
    {
        
        
        Oxy2.value = currentOxy2/ maxOxy;
        Oxy1.value = currentOxy1 / maxOxy;
        if (isFat)
        {
            currentFatigue -= Time.deltaTime * fatIncrease;
            fatigueSlider.value = currentFatigue / maxFatigue;
        }
        
        if (currentFatigue <= 0 || currentOxy1 <=0)
        {
            currentHp -= Time.deltaTime * HpIncrease;
            HPSlider.value = currentHp / MaxHp;
        }

        if (!GameManager.Instance.inSpaceShip)
        {
            if (currentOxy2 >= 0)
            {
                currentOxy2 -= Time.deltaTime * OxyIncrease;
            }
            else
            {
                currentOxy1 -= Time.deltaTime * OxyIncrease;
            }
        }
        else
        {
            if (currentOxy2 < maxOxy)
            {
                if (currentOxy1 < maxOxy)
                {
                    currentOxy1 += Time.deltaTime * OxyIncrease;
                }
                else
                {
                    currentOxy2 += Time.deltaTime * OxyIncrease;
                }
            }
        }
    }
    
}
