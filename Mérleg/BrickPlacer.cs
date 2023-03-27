using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrickPlacer : MonoBehaviour
{
    public GameObject brick;

    public bool canPlaceBricks;
    
    public GameObject buttonText;
    public AudioSource brickSound;
    public TextMeshProUGUI forceOverMouse;

    private void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.red;

        //-------FORCE DISPLAY OVER MOUSE-------
        if (!forceOverMouse.gameObject.activeSelf) forceOverMouse.gameObject.SetActive(true);
        forceOverMouse.text = CheckForceAtPosition().ToString("F0") + " Nm";
        //--------------------------------------

        if (Input.GetMouseButtonDown(1) && canPlaceBricks)
        {
            RemoveBrick();
        }
    }

    private void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.black;

        forceOverMouse.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && canPlaceBricks)
        {
            PlaceBrick();
        }
    }

    private void PlaceBrick()
    {
        brickSound.Play();
        var newBrick = Instantiate(brick, transform.position +
            new Vector3(0, .2f*(transform.childCount + 1) ,0), Quaternion.identity);
        newBrick.transform.parent = gameObject.transform;
        //Debug.Log(transform.childCount);
        gameObject.GetComponent<ForceCalculate>().RefreshEquationDisplay();
    }

    private void RemoveBrick()
    {
        if (transform.childCount > 0)
        {
            brickSound.Play();
            DestroyImmediate(transform.GetChild(transform.childCount-1).gameObject);
            //Immediately destroys the object, without waiting for the end of the frame.
            //Another option might be to detach the child from the parent, and then destroy it.
        }
        //Debug.Log(transform.childCount);
        gameObject.GetComponent<ForceCalculate>().RefreshEquationDisplay();
    }

    private float CheckForceAtPosition()
    {
        //childcount * weight of one brick * gravitational constant * distance from the center of the scale
        return transform.childCount * 5f *  10f * Mathf.Abs(transform.position.x);
    }
}