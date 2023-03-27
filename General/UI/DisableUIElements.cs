using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUIElements : MonoBehaviour
{
    public GameObject[] UIElements;
    private bool isDisabled = false;

    public void Disable()
    {
        if (!isDisabled)
        {
            foreach (GameObject element in UIElements)
            {
                element.SetActive(false);
                isDisabled = true;
            }
        }
        else
        {
            foreach (GameObject element in UIElements)
            {
                element.SetActive(true);
                isDisabled = false;
            }
        }
    }
}
