using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJumpBar : MonoBehaviour
{
    public static event Action<UIJumpBar> OnUpdateUIBar;

    public GameObject sliderGO;
    public Slider sliderBar;

    private void OnEnable()
    {
        PlayerController.UpdateUI += GetCurrentFill;
    }

    private void OnDisable()
    {
        PlayerController.UpdateUI -= GetCurrentFill;
    }

    //Function called in PlayerController
    private void GetCurrentFill(float current, float min, float max, bool enlarge)
    {
        OnUpdateUIBar?.Invoke(this);

        //Set the values of the slider bar
        sliderBar.minValue = min;
        sliderBar.maxValue = max;
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
