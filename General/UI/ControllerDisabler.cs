using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDisabler : MonoBehaviour
{
    public GameObject controller;

    public void DisableController()
    {
        if (controller.activeSelf) controller.SetActive(false);
        else controller.SetActive(true);
    }
}
