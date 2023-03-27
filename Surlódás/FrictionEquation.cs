using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*A script to display the values of the equation*/
public class FrictionEquation : MonoBehaviour
{
    public Slider frictionSlider;
    public Slider weightSlider;
    public TextMeshProUGUI equation;

    private void Update()
    {
        float friction = frictionSlider.value;
        float weight = weightSlider.value;
        float frictionForce = weight * 10 * friction;
        equation.text = frictionForce.ToString("F0") + " N (kerekítve) = " + 
        (weight * 10).ToString("F0") + " N * " + friction.ToString("F2");
    }
}
