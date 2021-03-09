using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UIJumpBar : MonoBehaviour
{
    public GameObject sliderGO;
    public Slider sliderBar;

    //Function called in PlayerController
    public void GetCurrentFill(float current, float min, float maximum, bool enlarge)
    {
        //Set the values of the slider bar
        sliderBar.minValue = min;
        sliderBar.maxValue = maximum;
        sliderBar.value = current;

        //make ui bigger as jump size increases or decreases
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
