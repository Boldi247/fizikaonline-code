using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SizeSliderHandler : MonoBehaviour
{
    public GameObject weight;
    public GameObject weightTextParent;
    private float initialWeightInGrams = 300f;
    private float weightInGrams = 300f;

    private float initialCubicCM = 5 * 5 * 5;
    public TextMeshProUGUI cubicCMVal;

    private float calculatedSize = 5 * 5 * 5;

    public void OnSliderInteraction(float value)
    {
        weight.transform.localScale = new Vector3(value, value, 0);
        weightInGrams = initialWeightInGrams * value;
        weightTextParent.GetComponent<TextMeshPro>().text = weightInGrams.ToString("F0") + " g\n" +
            (weightInGrams / 1000).ToString("F1") + " kg";
        calculatedSize = initialCubicCM * value;
        cubicCMVal.text = calculatedSize.ToString("F0") + " cm" + "\u00B3".ToString();
    }

    public float GetWeightInGrams()
    {
        return weightInGrams;
    }

    public float GetCalculatedSize()
    {
        return calculatedSize;
    }
}
