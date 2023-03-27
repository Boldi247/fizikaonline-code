using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*A script to display the value of the friction*/
public class FrictionSlider : MonoBehaviour
{
    public TextMeshProUGUI frictionTextVal;
    
    public void OnFrictionSliderInteraction(float value)
    {
        frictionTextVal.text = value.ToString("F2") + " \x00B5";
    }
}
