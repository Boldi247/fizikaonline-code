using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForceCalculate : MonoBehaviour
{
    private List<Transform> leftChildrens = new List<Transform>();
    private List<Transform> rightChildrens = new List<Transform>();
    public GameObject scale;
    private float brickWeight = 5f;
    private float distPlacePoints = .6f;

    public TextMeshProUGUI equationText;

    public void RefreshEquationDisplay()
    {
        leftChildrens.Clear();
        rightChildrens.Clear();

        for (int i = 0; i < scale.transform.childCount; i++)
        {
            if (i <= 3)
            {
                leftChildrens.Add(scale.transform.GetChild(i));
            }
            else
            {
                rightChildrens.Add(scale.transform.GetChild(i));
            }
        }
        leftChildrens.Reverse();
        float leftForce = CalculateForce(leftChildrens);
        float rightForce = CalculateForce(rightChildrens);

        equationText.text = leftForce.ToString("F0") + " Nm (bal oldal)" + 
            checkIfEqual(leftForce, rightForce) + rightForce.ToString("F0") + " Nm (jobb oldal)";
    }

    private string checkIfEqual(float left, float right) {
        if ((int)left == (int)right) {
            return " = ";
        } else if ((int)left > (int)right) {
            return " > ";
        } else {
            return " < ";
        }
    }

    private float CalculateForce(List<Transform> childrens)
    {
        float force = 0;
        for (int i = 0; i < childrens.Count; i++)
        {
            force += ((i + 1) * distPlacePoints) * (childrens[i].childCount * brickWeight * 10);
        }
        return force;
    }
}
