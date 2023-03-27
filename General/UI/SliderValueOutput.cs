using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderValueOutput : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public string textBeforeValue = "";
    public string textAfterValue = "";
    public int decimalPlaces = 0;

    public void SliderInteraction(float value)
    {
        valueText.text = textBeforeValue + " " + value.ToString("F" + decimalPlaces) + " " + textAfterValue;
    }
}
