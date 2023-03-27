using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HidrostaticPressure : MonoBehaviour
{
    private float surfaceY;
    private float yOffsetFromSurface = 3f;

    public GameObject distObj, depthAndPressureT;
    public GameObject InformationsPanel;
    public TextMeshProUGUI equation;

    private void Start()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        surfaceY = stageDimensions.y;
    }

    private void Update()
    {
        float distanceFromSurface = surfaceY - transform.position.y;
        distObj.transform.localScale = new Vector3(1, distanceFromSurface, 1);
        float roundedDist = (float) Math.Round(distanceFromSurface + yOffsetFromSurface, 1, MidpointRounding.AwayFromZero);
        float pressure = roundedDist * 10f;

        depthAndPressureT.GetComponent<TextMeshPro>().text = roundedDist.ToString("F1") + " m\n" +
            pressure.ToString("F0") + " kPa";

        if (InformationsPanel.activeSelf)
        {
            equation.text = (pressure * 1000).ToString("F0") + " Pa = 1000 kg/m" + "\u00B3".ToString() + " * " + roundedDist.ToString("F1") +
                " m * 10 m/s" + "\u00B2".ToString() + "\n\n" + pressure.ToString("F0") + " kPa";
        }
    }
}
