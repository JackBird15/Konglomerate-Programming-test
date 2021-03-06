using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UIJumpBar : MonoBehaviour
{
    public GameObject sliderGO;
    public Slider sliderBar;

    public void GetCurrentFill(float current,float min, float maximum)
    {
        sliderBar.minValue = min;
        sliderBar.maxValue = maximum;
        sliderBar.value = current;

        //make ui bigger as jump size increases
       sliderGO.transform.localScale = Vector3.MoveTowards(sliderGO.transform.localScale, new Vector3(0.8f,0.8f,0.8f), Time.deltaTime * 3);
    }
}
