using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtlagSeb_SliderController : MonoBehaviour
{
    public GameObject dividerObject;
    public GameObject startObject;
    public Slider dividerDistanceSlider;
    public TextMeshProUGUI dividerDistanceValText;

    public Slider firstPathTimeSlider;
    public Slider secondPathTimeSlider;
    public TextMeshProUGUI firstPathTimeValText;
    public TextMeshProUGUI secondPathTimeValText;

    private void Start()
    {
        dividerDistanceSlider.value = 6f;
    }

    public void DividerDistanceSliderValueChanged(float value)
    {
        MoveDivider(value);
        ChangeDividerValText(value);
    }

    public void FirstPathTimeSliderValueChanged(float value)
    {
        ChangeFirstPathTimeValText(value);
    }

    public void SecondPathTimeSliderValueChanged(float value)
    {
        ChangeSecondPathTimeValText(value);
    }

    private void ChangeDividerValText(float value)
    {
        dividerDistanceValText.text = "s(m) = " + value.ToString();
    }

    private void ChangeFirstPathTimeValText(float value)
    {
        firstPathTimeValText.text = "t1(s) = " + value.ToString();
    }

    private void ChangeSecondPathTimeValText(float value)
    {
        secondPathTimeValText.text = "t2(s) = " + value.ToString();
    }

    private void MoveDivider(float value)
    {
        var newPos = startObject.transform.position.x + value;
        dividerObject.transform.position = new Vector3(newPos, dividerObject.transform.position.y, dividerObject.transform.position.z);
    }

}
