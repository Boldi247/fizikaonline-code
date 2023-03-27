using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*A script to set the weight of the box*/
public class WeightSlider : MonoBehaviour
{
    public TextMeshProUGUI containerValue;
    public GameObject boxChildrenContainer;
    private TextMeshPro boxKgVal;

    public GameObject downwardsForceTextContainer;
    private TextMeshPro downwardsForceText;

    public GameObject upwardsForceTextContainer;
    private TextMeshPro upwardsForceText;

    private void Awake()
    {
        boxKgVal = boxChildrenContainer.GetComponent<TextMeshPro>();
        downwardsForceText = downwardsForceTextContainer.GetComponent<TextMeshPro>();
        upwardsForceText = upwardsForceTextContainer.GetComponent<TextMeshPro>();
    }

    public void OnWeightSliderInteraction(float value)
    {
        containerValue.text = value.ToString("F0") + " kg";
        boxKgVal.text = value.ToString("F0") + " kg";

        downwardsForceText.text = (value * 10f).ToString("F0") + " N";
        upwardsForceText.text = (value * 10f).ToString("F0") + " N";
    }
}
