using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedHealthBar : MonoBehaviour
{
    public PlayerMovement player;
    public Image fillImage;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        
    }

    public void DisplayPlayerHealth()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        float fillValue = player.currentHealth / player.maxHealth;

        if (fillValue <= slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }
        else if (fillValue > slider.minValue / 3)
        {
            fillImage.color = Color.green;
        }

        slider.value = fillValue;
    }
}
