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
    public float HpIncrease = 3;
    public float MaxHp;
    public float currentHp;
    public float fatIncrease = 0.4f;
    public float maxFatigue = 100f;
    public float currentFatigue;
    public bool isFat = false;
    public float dayIncrease = 30f;
    private FliterSystem fs;
    void Start()
    {
        currentOxy1 = maxOxy;
        currentOxy2 = maxOxy;
        currentHp = MaxHp;
        currentFatigue = maxFatigue;
        fs = FindAnyObjectByType<FliterSystem>();
    }

    void Update()
    {


        Oxy2.value = currentOxy2 / maxOxy;
        Oxy1.value = currentOxy1 / maxOxy;
        HPSlider.value = currentHp / MaxHp;
        fatigueSlider.value = currentFatigue / maxFatigue;

        if (isFat)
        {
            currentFatigue -= Time.deltaTime * fatIncrease;
        }

        if (currentFatigue <= 0 || currentOxy1 <= 0)
        {
            currentHp -= Time.deltaTime * HpIncrease;

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
        else if (GameManager.Instance.inSpaceShip && fs != null && !fs.isbroken)
        {
            currentOxy1 = maxOxy;
            currentOxy2 = maxOxy;
        }
        else
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
        if (HPSlider.value <= 0)
        {
            Time.timeScale = 0;
            UIManager.Instance.GameOverUI.SetActive(true);
            GameManager.Instance.MouseCursor(true);
            GameManager.Instance.ismove = false;
            GameManager.Instance.isCamera = false;
            GameManager.Instance.noOpen = true;
            GameManager.Instance.noInventoryOpen = true;
        }
    }
    
    
}
