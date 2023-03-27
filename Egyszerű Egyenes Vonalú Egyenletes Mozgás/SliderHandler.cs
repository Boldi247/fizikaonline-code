using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sebessegErtek;
    [SerializeField] private TextMeshProUGUI utErtek;
    [SerializeField] private TextMeshProUGUI idoErtek;
    [SerializeField] private GameObject endObject;
    [SerializeField] private GameObject startObject;

    public void SebessegSliderInteraction(float value)
    {
        sebessegErtek.text = value.ToString("F2");
    }

    public void UtSliderInteraction(float value)
    {
        utErtek.text = value.ToString("F2");
        MoveEndObject(value);
    }

    public void IdoSliderInteraction(float value)
    {
        idoErtek.text = value.ToString("F2");
    }

    private void MoveEndObject(float value)
    {
        var newPos = startObject.transform.position.x + value;
        endObject.transform.position =
            new Vector3(newPos, endObject.transform.position.y, endObject.transform.position.z);
    }
}
