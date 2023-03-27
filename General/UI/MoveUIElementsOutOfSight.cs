using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIElementsOutOfSight : MonoBehaviour
{
    public GameObject[] uiElements;
    private Dictionary<GameObject, Vector3> uiElementsPositions = new Dictionary<GameObject, Vector3>();
    private bool pressedOnce = false;

    private void Start()
    {
        foreach (GameObject uiElement in uiElements)
        {
            Vector3 uiElementPosition = uiElement.transform.position;
            uiElementsPositions.Add(uiElement, uiElementPosition);
        }
    }

    public void MoveUIElements()
    {
        switch (pressedOnce)
        {
            case false:
                pressedOnce = true;
                foreach (GameObject uiElement in uiElements)
                {
                    uiElement.transform.position =
                        new Vector3(uiElement.transform.position.x, uiElement.transform.position.y, -10000);
                }
                return;
            case true:
                pressedOnce = false;
                foreach (GameObject uiElement in uiElements)
                {
                    uiElement.transform.position = uiElementsPositions[uiElement];
                }
                return;
        }
        
    }
}
