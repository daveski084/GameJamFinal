using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxPlayerTemp(float temp)
    {
        slider.maxValue = temp;
    }
    public void SetPlayerTemp(float temp)
    {
        slider.value = temp;
    }
}
