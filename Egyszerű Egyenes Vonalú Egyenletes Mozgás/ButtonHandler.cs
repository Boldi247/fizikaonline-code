using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject sebessegPanel;
    [SerializeField] private GameObject utPanel;
    [SerializeField] private GameObject idoPanel;

    [SerializeField] private Slider sebessegSlider;
    [SerializeField] private Slider utSlider;
    [SerializeField] private Slider idoSlider;

    [SerializeField] private GameObject car;

    [SerializeField] private GameObject startMarker;
    [SerializeField] private GameObject endMarker;

    [SerializeField] private TextMeshProUGUI text_sebessegErtek;
    [SerializeField] private TextMeshProUGUI text_utErtek;
    [SerializeField] private TextMeshProUGUI text_idoErtek;

    private float startTime;

    private Vector3 endMarkerX;
    private Vector3 startMarkerX;

    private float sebesseg;
    private float ut;
    private float ido;

    private bool movementEnabled;

    private Button startButton;

    private void Start()
    {
        startButton = GetComponent<Button>();
    }

    public void OnButtonPressed()
    {
        SliderAndButtonInteraction(false);
        
        endMarkerX = new Vector3(endMarker.transform.position.x, car.transform.position.y, car.transform.position.z);
        startMarkerX = new Vector3(startMarker.transform.position.x, car.transform.position.y, car.transform.position.z);

        startTime = Time.time;
        if (!sebessegPanel.activeInHierarchy)
        {
            CalculateSebesseg();
        }
        else
        {
            CalculateIdo();
        }
        
        DisplayVariables();
    }

    private void DisplayVariables()
    {
        text_sebessegErtek.text = sebesseg.ToString("F2") + " m/s (" + (sebesseg * 3.6).ToString("F2") + " km/h)";
        text_utErtek.text = ut.ToString("F2") + " m";
        text_idoErtek.text = ido.ToString("F2") + " s";
    }
    
    private void SliderAndButtonInteraction(bool enabled)
    {
        if (!enabled)
        {
            sebessegSlider.interactable = false;
            utSlider.interactable = false;
            idoSlider.interactable = false;
            startButton.interactable = false;
        }
        else
        {
            sebessegSlider.interactable = true;
            utSlider.interactable = true;
            idoSlider.interactable = true;
            startButton.interactable = true;
        }     
    }

    private void CalculateSebesseg()
    {
        ut = utSlider.value;
        ido = idoSlider.value;
        sebesseg = ut / ido;
        movementEnabled = true;
    }

    private void CalculateIdo()
    {
        ut = utSlider.value;
        sebesseg = sebessegSlider.value;
        ido = ut / sebesseg;
        movementEnabled = true;
    }

    private void Update()
    {
        if (movementEnabled)
        {
            float distCovered = (Time.time - startTime) * sebesseg;
            float fracJourney = distCovered / utSlider.value;

            car.transform.position = Vector3.Lerp(startMarkerX, endMarkerX, fracJourney);
            if (car.transform.position.x == endMarker.transform.position.x)
            {
                movementEnabled = false;
                StartCoroutine(ResetState());
            }
        }
    }

    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(1);
        car.transform.position = startMarkerX;
        SliderAndButtonInteraction(true);
    }
}
