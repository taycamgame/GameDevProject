using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateHealthBar(float currentValue, float maximumValue)
    {
        slider.value = currentValue / maximumValue;
        if (slider.gameObject.activeSelf == false)
        {
            slider.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        if (slider != null)
        {
            if (slider.value == 1)
            {
                slider.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
