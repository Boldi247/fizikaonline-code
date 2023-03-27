using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetButtonsToInteractable : MonoBehaviour
{
    public GameObject[] buttons;
    public bool enableOnClick;

    public void SetButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<UnityEngine.UI.Button>().interactable = enableOnClick;
        }
    }
}