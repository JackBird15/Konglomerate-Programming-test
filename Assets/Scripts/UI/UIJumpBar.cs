using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UIJumpBar : MonoBehaviour
{
    public GameObject sliderGO;
    public Slider sliderBar;

    public void GetCurrentFill(float current, float min, float maximum, bool enlarge)
    {
        sliderBar.minValue = min;
        sliderBar.maxValue = maximum;
        sliderBar.value = current;

        //make ui bigger as jump size increases
        if (enlarge)
        {
            sliderGO.transform.localScale = Vector3.MoveTowards(sliderGO.transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), Time.deltaTime * 0.2f);
        }
        else
        {
            sliderGO.transform.localScale = Vector3.MoveTowards(sliderGO.transform.localScale, new Vector3(0.7f, 0.7f, 0.7f), Time.deltaTime * 0.2f);
        }
    }
}
