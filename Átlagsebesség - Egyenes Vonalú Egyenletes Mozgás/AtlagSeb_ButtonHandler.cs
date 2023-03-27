using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtlagSeb_ButtonHandler : MonoBehaviour
{
    public GameObject car;
    
    public GameObject startObject;
    public GameObject middleObject;
    public GameObject endObject;

    public Slider dividerDistanceSlider;
    public Slider firstPathTimeSlider;
    public Slider secondPathTimeSlider;
    private Button startButton;

    private float speed1;
    private float speed2;
    private float time1;
    private float time2;
    private float distance1;
    private float distance2;

    private float startTime;
    private bool firstMovementEnabled;
    private bool secondMovementEnabled;

    private Vector3 startMarkerX;
    private Vector3 middleMarkerX;
    private Vector3 endMarkerX;

    public TextMeshProUGUI firstSpeedText;
    public TextMeshProUGUI secondSpeedText;
    public TextMeshProUGUI avgSpeedText;
    public TextMeshProUGUI firstDistText;
    public TextMeshProUGUI secondDistText;

    private void Start()
    {
        startButton = GetComponent<Button>();
    }
    
    public void onStartButtonPressed()
    {
        ClearVariableListOnPanel();
        
        startMarkerX = new Vector3(startObject.transform.position.x, car.transform.position.y, car.transform.position.z);
        middleMarkerX = new Vector3(middleObject.transform.position.x, car.transform.position.y, car.transform.position.z);
        endMarkerX = new Vector3(endObject.transform.position.x, car.transform.position.y, car.transform.position.z);
        
        startTime = Time.time;
        
        CalculateSpeeds();
        EnableUserInput(false);
    }

    private void CalculateSpeeds()
    {
        distance1 = middleObject.transform.position.x - startObject.transform.position.x;
        distance2 = endObject.transform.position.x - middleObject.transform.position.x;
        time1 = firstPathTimeSlider.value;
        time2 = secondPathTimeSlider.value;
        speed1 = distance1 / time1;
        speed2 = distance2 / time2;

        firstMovementEnabled = true;
    }

    private void Update()
    {
        if (firstMovementEnabled)
        {
            float distanceCovered = (Time.time - startTime) * speed1;
            float fractionOfJourney = distanceCovered / distance1;

            car.transform.position = Vector3.Lerp(startMarkerX, middleMarkerX, fractionOfJourney);
            if (car.transform.position.x == middleMarkerX.x)
            {
                startTime = Time.time;
                firstMovementEnabled = false;
                secondMovementEnabled = true;
            }
        }
        if (secondMovementEnabled)
        {
            float distanceCovered = (Time.time - startTime) * speed2;
            float fractionOfJourney = distanceCovered / distance2;

            car.transform.position = Vector3.Lerp(middleMarkerX, endMarkerX, fractionOfJourney);
            if (car.transform.position.x == endMarkerX.x)
            {
                secondMovementEnabled = false;
                StartCoroutine(ResetState());
            }
        }
    }
    
    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(1f);
        car.transform.position = startMarkerX;
        EnableUserInput(true);
        DisplayValuesOnPanel();
    }

    private void EnableUserInput(bool toggler)
    { 
        if (!toggler)
        {
            dividerDistanceSlider.interactable = false;
            firstPathTimeSlider.interactable = false;
            secondPathTimeSlider.interactable = false;
            startButton.interactable = false;
        }
        else
        {
            dividerDistanceSlider.interactable = true;
            firstPathTimeSlider.interactable = true;
            secondPathTimeSlider.interactable = true;
            startButton.interactable = true;
        }
    }

    private void ClearVariableListOnPanel()
    {
        firstSpeedText.text = "";
        secondSpeedText.text = "";
        avgSpeedText.text = "";
        firstDistText.text = "";
        secondDistText.text = "";
    }
    
    private void DisplayValuesOnPanel()
    {
        firstSpeedText.text = speed1.ToString("F2") + "m/s (" + (speed1 * 3.6).ToString("F2")+ " km/h)";
        secondSpeedText.text = speed2.ToString("F2") + "m/s (" + (speed2 * 3.6).ToString("F2") + " km/h)";
        avgSpeedText.text = ((distance1 + distance2) / (time1 + time2)).ToString("F2") + "m/s (" +
            ((distance1 + distance2) / (time1 + time2) * 3.6).ToString("F2") + " km/h)";
        firstDistText.text = distance1.ToString("F0") + "m";
        secondDistText.text = distance2.ToString("F0") + "m";
    }
}
