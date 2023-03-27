using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ToolbarSelect : MonoBehaviour, IPointerDownHandler
{
    public GameObject liquid;

    public GameObject candlePrefab;
    public GameObject rockPrefab;
    public GameObject corkPrefab;

    private GameObject obj;

    private enum ObjectType {Candle, Rock, Cork};
    private ObjectType objectType;

    //UI OBJECTS
    public TextMeshProUGUI UI_selectedObject;
    private float UI_liquidDensity, UI_objectDensity;
    public TextMeshProUGUI UI_sign;
    //----------


    //Handle clicking on the toolbar item -- which is a UI element
    public void OnPointerDown(PointerEventData eventData)
    {
        CreateObject();
        DisplayObjectInformationsUI();
    }

    private void CreateObject()
    {
        //if there is a draggable object already in the scene, delete it
        GameObject findObject = GameObject.FindGameObjectWithTag("draggable");
        if (findObject != null)
        {
            Destroy(findObject);
        }

        //get the worldposition of the mouse and store it in a variable
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //if there are no exesting objects, create a new one, depending on their tags
        if (gameObject.tag == "candle")
        {
            obj = Instantiate(candlePrefab, mousePos, Quaternion.identity);
            objectType = ObjectType.Candle;
        }
        if (gameObject.tag == "rock")
        {
            obj = Instantiate(rockPrefab, mousePos, Quaternion.identity);
            objectType = ObjectType.Rock;
        }
        if (gameObject.tag == "cork")
        {
            obj = Instantiate(corkPrefab, mousePos, Quaternion.identity);
            objectType = ObjectType.Cork;
        }

        obj.GetComponent<Drag>().isCollidingWithLiquid = false;
        SetAppropriateSettings();
    }

    private void SetAppropriateSettings() 
    {
        if (liquid.tag == "Water")
        {
            switch (objectType)
            {
                case ObjectType.Candle:
                    Surfacing();
                    return;
                case ObjectType.Rock:
                    Sinking();
                    return;
                case ObjectType.Cork:
                    Surfacing();
                    return;
            }
        }
        else if (liquid.tag == "Oil")
        {
            switch (objectType)
            {
                case ObjectType.Candle:
                    Levitating();
                    return;
                case ObjectType.Rock:
                    Sinking();
                    return;
                case ObjectType.Cork:
                    Surfacing();
                    return;
            }
        }
    }

    private void Surfacing()
    {
        if (objectType != ObjectType.Cork) obj.GetComponent<Rigidbody2D>().mass = 0.4f;
        else obj.GetComponent<Rigidbody2D>().mass = 0.05f;
        liquid.GetComponent<BuoyancyEffector2D>().surfaceLevel = 0.4f;
    }

    private void Sinking()
    {
        obj.GetComponent<Rigidbody2D>().mass = 3f;
        liquid.GetComponent<BuoyancyEffector2D>().surfaceLevel = 0.4f;
    }

    private void Levitating()
    {
        obj.GetComponent<Rigidbody2D>().mass = 0.4f;
        liquid.GetComponent<BuoyancyEffector2D>().surfaceLevel = 0.2f;
    }

    //UI Scripts below
    private void DisplayObjectInformationsUI()
    {
        if (objectType == ObjectType.Candle) 
        {
            UI_selectedObject.text = "Gyertya (0.9 g/cm" + (char)0x00B3 + ")";
            UI_objectDensity = 0.9f;
        }
        else if (objectType == ObjectType.Rock)
        {
            UI_selectedObject.text = "Kő (2.6 g/cm" + (char)0x00B3 + ")";
            UI_objectDensity = 2.6f;
        }
        else if (objectType == ObjectType.Cork) 
        {
            UI_selectedObject.text = "Parafa dugó (0.24 g/cm" + (char)0x00B3 + ")";
            UI_objectDensity = 0.24f;
        }

        if (liquid.tag == "Water") UI_liquidDensity = 1f;
        else if (liquid.tag == "Oil") UI_liquidDensity = 0.9f;

        RotateSign();
    }

    private void RotateSign()
    {
        if (UI_objectDensity > UI_liquidDensity)
        {
            UI_sign.text = "V";
            UI_sign.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (UI_objectDensity < UI_liquidDensity)
        {
            UI_sign.text = "V";
            UI_sign.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (UI_objectDensity == UI_liquidDensity)
        {
            UI_sign.text = "=";
            UI_sign.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}