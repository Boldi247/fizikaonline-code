using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumerOfObjects : MonoBehaviour
{
    public GameObject topBall;
    public GameObject bottomBall;
    public GameObject leftBall;
    public GameObject rightBall;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject topArrow;
    public GameObject bottomArrow;

    public void DropDownSelector(int index)
    {
        switch (index)
        {
            case 0:
                topBall.GetComponent<Renderer>().enabled = true;
                bottomBall.GetComponent<Renderer>().enabled = true;
                leftBall.GetComponent<Renderer>().enabled = true;
                rightBall.GetComponent<Renderer>().enabled = true;

                topArrow.SetActive(true);
                bottomArrow.SetActive(true);
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);

                break;
            case 1:
                topBall.GetComponent<Renderer>().enabled = true;
                bottomBall.GetComponent<Renderer>().enabled = true;
                leftBall.GetComponent<Renderer>().enabled = false;
                rightBall.GetComponent<Renderer>().enabled = false;
                
                topArrow.SetActive(true);
                bottomArrow.SetActive(true);
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);

                break;
            case 2:
                topBall.GetComponent<Renderer>().enabled = false;
                bottomBall.GetComponent<Renderer>().enabled = false;
                leftBall.GetComponent<Renderer>().enabled = false;
                rightBall.GetComponent<Renderer>().enabled = true;

                topArrow.SetActive(false);
                bottomArrow.SetActive(false);
                leftArrow.SetActive(false);
                rightArrow.SetActive(true);

                break;
        }
    }
}
