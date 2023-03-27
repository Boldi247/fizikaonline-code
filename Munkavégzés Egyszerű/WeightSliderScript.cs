using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeightSliderScript : MonoBehaviour
{
    public TextMeshProUGUI textVal;
    public GameObject ball;

    public void OnSliderValueChanged(float value)
    {
        textVal.text = value.ToString("F0") + " kg";
        ball.GetComponent<Rigidbody2D>().mass = value;
    }
}
