using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleRotation : MonoBehaviour
{
    private List<Transform> leftChildrens = new List<Transform>();
    private List<Transform> rightChildrens = new List<Transform>();
    public GameObject scale;

    private float brickWeight = 5f;
    private float distPlacePoints = .6f;

    private bool pressedOnce = false;

    public GameObject buttonText;

    public AudioSource clickSound;

    private BrickPlacer[] brickPlacers;

    private void Awake()
    {
        brickPlacers = FindObjectsOfType<BrickPlacer>();
        foreach (BrickPlacer brickPlacer in brickPlacers)
        {
            brickPlacer.canPlaceBricks = true;
        }
    }

    public void OnStartButtonPressed()
    {
        clickSound.Play();
        if (!pressedOnce)
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
            float force = CalculateForce();
            TiltScale(force);     
            pressedOnce = true;
            buttonText.GetComponent<Text>().text = "Súlyok pakolása";
            gameObject.GetComponent<Image>().color = Color.red;

            foreach (BrickPlacer brickPlacer in brickPlacers)
            {
                brickPlacer.canPlaceBricks = false;
            }
        }
        else
        {
            scale.transform.rotation = Quaternion.Euler(0, 0, 0);
            pressedOnce = false;
            buttonText.GetComponent<Text>().text = "Mérlegelés";
            gameObject.GetComponent<Image>().color = Color.white;

            foreach (BrickPlacer brickPlacer in brickPlacers)
            {
                brickPlacer.canPlaceBricks = true;
            }
        }
    }

    private void TiltScale(float force)
    {
        if (Math.Abs(force) > 50)
        {
            //full rotation (16 degrees)
            scale.transform.rotation = Quaternion.Euler(0, 0, -Math.Sign(force) * 16);
        }
        else if (Math.Abs(force) <= 50 && force != 0)
        {
            //partial rotation (MAX 12 degrees)
            float rotation = (float)(force / 50 * 12);
            scale.transform.rotation = Quaternion.Euler(0, 0, -rotation);
        }
        else if (force == 0f)
        {
            scale.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    private float CalculateForce()
    {
        /*if the function returns a negative number, it means that the scale is tilting to the left
          if it returns a positive number, it is tilting to the right
          if it is 0, the scale's weights are evenly distributed and the scale does not tilt*/
        
        float leftForce= 0f;
        float rightForce = 0f;

        for (int i = 0; i < leftChildrens.Count; i++)
        {
            leftForce += ((i + 1) * distPlacePoints) * (leftChildrens[i].childCount * brickWeight * 10);
        }
        leftForce = -leftForce;

        for (int i = 0; i < rightChildrens.Count; i++)
        {
            rightForce += ((i + 1) * distPlacePoints) * (rightChildrens[i].childCount * brickWeight * 10);
        }

        return (leftForce + rightForce);
    }
    
    private void DebuggerFunction()
    {
        //Debug.Log("Left Childrens: " + leftChildrens.Count);
        foreach (Transform child in leftChildrens)
        {
            //Debug.Log(child.childCount);
        }
        //Debug.Log("Right Childrens: " + rightChildrens.Count);
        foreach (Transform child in rightChildrens)
        {
            //Debug.Log(child.childCount);
        }
    }
}
