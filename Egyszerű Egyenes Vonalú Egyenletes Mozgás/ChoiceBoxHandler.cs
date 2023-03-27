using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceBoxHandler : MonoBehaviour
{
    [SerializeField] private GameObject sebesseg;
    [SerializeField] private GameObject ut;
    [SerializeField] private GameObject ido;

    [SerializeField] private Slider sebessegSlider;
    [SerializeField] private Slider utSlider;
    [SerializeField] private Slider idoSlider;
    
    private void Start()
    {
        sebesseg.SetActive(false);
        utSlider.value = 12;
    }

    public void HandleChoiceBox(int index)
    {
        clearAllSliders();
        switch (index)
        {
            case 0:
                sebesseg.SetActive(false);
                ut.SetActive(true);
                ido.SetActive(true);
                break;
            case 1:
                sebesseg.SetActive(true);
                ut.SetActive(true);
                ido.SetActive(false);
                break;
        }
    }

    private void clearAllSliders()
    {
        sebessegSlider.value = 0;
        utSlider.value = 12;
        idoSlider.value = 0;
    }

}