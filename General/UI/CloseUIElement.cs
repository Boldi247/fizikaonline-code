using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIElement : MonoBehaviour
{
    public GameObject panel;
    public AudioSource clickSound;

    public void OnExitButtonPressed()
    {
        clickSound.Play();
        panel.SetActive(false);
    }
}
