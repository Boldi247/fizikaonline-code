using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckSubmersion : MonoBehaviour
{
    public GameObject water;
    public Slider weightSizeSlider;
    public GameObject weightTextParent;
    private float ratioOfSubmerging = 0;

    public GameObject overflowBeakerWater;
    public GameObject waterSpill;

    private VerticalDragNoRB verticalDragNoRB;

    public AudioSource pourSound;
    private bool playedOnce = false;

    public GameObject overflowBeakerBottomTextParent;

    private void Start()
    {
        verticalDragNoRB = transform.GetComponent<VerticalDragNoRB>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weightSizeSlider.interactable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" /*&& verticalDragNoRB.GetDragged()*/)
        {
            ratioOfSubmerging = CheckRatioOfSubmergeing();
            CalculateLossOfMass();
            UpdateWaterLevelOfOverflowBeaker();
            DisplaySpill();
            DisplayText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            ratioOfSubmerging = 0;
        }
    }

    private void DisplayText()
    {
        float size = (weightSizeSlider.GetComponent<SizeSliderHandler>().GetCalculatedSize() * ratioOfSubmerging);
        overflowBeakerBottomTextParent.GetComponent<TextMeshPro>().text =
            size.ToString("F0") + " cm" + "\u00B3".ToString() + "\n"
            + size.ToString("F0") + " g";
    }

    private void DisplaySpill()
    {
        if (verticalDragNoRB.GetDragged() && ratioOfSubmerging != 1f)
        {
            waterSpill.SetActive(true);
            if (!pourSound.isPlaying && !playedOnce)
            {
                pourSound.Play();
                playedOnce = true;
            }
        }
        else
        {
            waterSpill.SetActive(false);
            pourSound.Stop();
            playedOnce = false;
        }
    }

    private void UpdateWaterLevelOfOverflowBeaker()
    {
        double weightCubeCubic = Math.Pow(transform.localScale.x, 2);
        float waterYScale = (float)weightCubeCubic / 2;
        overflowBeakerWater.transform.localScale =
            new Vector3(overflowBeakerWater.transform.localScale.x, waterYScale*ratioOfSubmerging,
            overflowBeakerWater.transform.localScale.z);
    }

    public void CalculateLossOfMass()
    {
        float massOfObject = weightSizeSlider.GetComponent<SizeSliderHandler>().GetWeightInGrams();
        float sizeOfObject = weightSizeSlider.GetComponent<SizeSliderHandler>().GetCalculatedSize();

        float massOfWaterPushedOut = ratioOfSubmerging * sizeOfObject;
        float submergedMassOfObject = massOfObject - massOfWaterPushedOut;

        weightTextParent.GetComponent<TextMeshPro>().text = submergedMassOfObject.ToString("F0") + " g\n" + (submergedMassOfObject/1000).ToString("F1") + " kg";
    }

    private float CheckRatioOfSubmergeing()
    {
        float bottomOfObject = transform.position.y - transform.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        float topOfWater = water.transform.position.y + water.transform.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        float ratio = (topOfWater - bottomOfObject) / transform.GetComponent<BoxCollider2D>().bounds.size.y;
        
        if (ratio > 1)
        {
            ratio = 1;
        }
        else if (ratio < 0)
        {
            ratio = 0;
        }

        return ratio;
    }
}
