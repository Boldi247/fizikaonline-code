using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEnabler : MonoBehaviour
{
    public GameObject panel;
    public AudioSource clickSound;

    public Button disabled1, disabled2, disabled3;
    
    private void Start()
    {
        panel.SetActive(false);
    }

    public void OnButtonPressed()
    {
       clickSound.Play();
       EnablePanel();
    }
    
    private void EnablePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            disabled1.interactable = true;
            disabled2.interactable = true;
            disabled3.interactable = true;
        }
        else
        {
            panel.SetActive(true);
            disabled1.interactable = false;
            disabled2.interactable = false;
            disabled3.interactable = false;
        }
    }
}
