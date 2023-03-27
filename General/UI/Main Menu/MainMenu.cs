using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject mainMenu;
    
    public void ButtonPressed()
    {
        panelToOpen.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButtonPressed()
    {
        panelToOpen.SetActive(false);
        mainMenu.SetActive(true);
    }
}
