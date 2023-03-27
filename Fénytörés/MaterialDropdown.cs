using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialDropdown : MonoBehaviour
{
    public GameObject material;
    
    private float waterLambda = 1.33f;
    private float glassLambda = 1.5f;
    private float diamondLambda = 2.42f;

    private float activeLambda = 1.33f;

    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;

    //UI degrees
    public GameObject laserRotationParent;
    //----------

    public void Dropdown_IndexChanged(int index)
    {
        Color color;
        switch (index)
        {
            case 0:
                ColorUtility.TryParseHtmlString("#D5FFF9", out color);
                material.GetComponent<Renderer>().material.color = color;
                ResetRotations();
                activeLambda = waterLambda;
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#45DDFF", out color);
                material.GetComponent<Renderer>().material.color = color;
                ResetRotations();
                activeLambda = glassLambda;
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#1744E7", out color);
                material.GetComponent<Renderer>().material.color = color;
                ResetRotations();
                activeLambda = diamondLambda;
                break;
        }
    }

    private void ResetRotations()
    {
        circle1.transform.rotation = Quaternion.Euler(0, 0, 0);
        circle2.transform.rotation = Quaternion.Euler(0, 0, 0);
        circle3.transform.rotation = Quaternion.Euler(0, 0, 0);

        laserRotationParent.GetComponent<LaserRotation>().ResetOnDropdownSelect();
    }
    
    public float GetActiveLambda()
    {
        return activeLambda;
    }
}
