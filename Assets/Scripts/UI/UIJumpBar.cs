using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UIJumpBar : MonoBehaviour
{
    public Image fillBar;
    public Slider sliderBar;

    public void GetCurrentFill(float current,float min, float maximum)
    {
        sliderBar.minValue = min;
        sliderBar.maxValue = maximum;
        sliderBar.value = current;
    }
}
