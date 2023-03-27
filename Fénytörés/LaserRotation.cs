using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LaserRotation : MonoBehaviour
{
    public GameObject circleContainer;
    private float degree = 0;
    public GameObject reflectionCircleContainer;
    public GameObject lightBreakCircleContainer;
    public GameObject materials;
    private float airLambda = 1.0f;
    public GameObject lightBreakLaserBeam;

    //UI degreees
    public GameObject topLeftDegree, topRightDegree, bottomRightDegree;
    public TextMeshProUGUI topLeftDegreeVal, topRightDegreeVal, bottomRightDegreeVal;
    //-----------

    private void Update()
    {
        if (degree == 0 && bottomRightDegree.activeSelf)
        {
            bottomRightDegree.SetActive(false);
        }
    }
    
    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        RotateCircleContainer(mousePosition);
    }

    private void RotateCircleContainer(Vector3 mousePosition)
    {
        degree = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg - 180;
        degree = -degree;

        if (degree < 0)
        {
            degree += 360;
        }
        if (degree > 180)
        {
            degree -= 360;
        }
        
        if (degree < 0)
        {
            degree = 0;
        }
        if (degree > 90)
        {
            degree = 90;
        }
        circleContainer.transform.rotation = Quaternion.Euler(0, 0, -degree);
        reflectionCircleContainer.transform.rotation = Quaternion.Euler(0, 0, degree);
        LightBreak();

        TopDegreeRotate(90 - degree);
    }
    
    private void LightBreak()
    {
        float rot = Mathf.Sin((90 - degree) * Mathf.Deg2Rad) *
            (airLambda / materials.GetComponent<MaterialDropdown>().GetActiveLambda());
        rot = Mathf.Asin(rot) * Mathf.Rad2Deg;
        lightBreakCircleContainer.transform.rotation = Quaternion.Euler(0, 0, -(90 - rot));

        BottomDegreeRotate(rot);
        SetOpacity();
    }

    private void SetOpacity()
    {
        Color oldcolor = lightBreakLaserBeam.GetComponent<SpriteRenderer>().color;
        if (degree <= 10)
        {
            float ratio = degree / 10;
            lightBreakLaserBeam.GetComponent<SpriteRenderer>().color = new Color(oldcolor.r, oldcolor.g, oldcolor.b, ratio);
        }
        else
        {
            lightBreakLaserBeam.GetComponent<SpriteRenderer>().color = new Color(oldcolor.r, oldcolor.g, oldcolor.b, 1);
        }
    }
    
    private void TopDegreeRotate(float degree)
    {
        float val = (0.25f / 90f) * degree;
        topLeftDegree.GetComponent<Image>().fillAmount = val;
        topRightDegree.GetComponent<Image>().fillAmount = val;
        topLeftDegreeVal.text = degree.ToString("F0") + "°";
        topRightDegreeVal.text = degree.ToString("F0") + "°";
    }

    private void BottomDegreeRotate(float rot)
    {
        if (!bottomRightDegree.activeSelf) bottomRightDegree.SetActive(true);
        float val = (0.25f / 90f) * rot;
        bottomRightDegree.GetComponent<Image>().fillAmount = val;
        bottomRightDegreeVal.text = rot.ToString("F0") + "°";
    }

    public void ResetOnDropdownSelect()
    {
        topLeftDegree.GetComponent<Image>().fillAmount = 0.25f;
        topRightDegree.GetComponent<Image>().fillAmount = 0.25f;
        topLeftDegreeVal.text = "90°";
        topRightDegreeVal.text = "90°";
        degree = 0;
    }
}
