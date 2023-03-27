using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    private bool pressedOnce = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressedOnce)
        {
            audioSource.Play();
            pressedOnce = true;
        }
    }
}
