using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiquidSelector : MonoBehaviour
{
    private Color waterColor = new Color32(126, 231, 255, 168);
    private Color oilColor = new Color32(236, 236, 54, 168);

    public GameObject liquid;

    public TextMeshProUGUI UI_selectedLiquid;
    public TextMeshProUGUI UI_sign;

    //Apply it to the dropdown selector of liquid types
    public void Dropdown_IndexChanged(int index)
    {
        GameObject draggableObj = GameObject.FindGameObjectWithTag("draggable");
        if (draggableObj != null) Destroy(draggableObj);

        UI_sign.text = "";

        switch (index)
        {
            case 0:
                liquid.GetComponent<SpriteRenderer>().color = waterColor;
                liquid.GetComponent<BuoyancyEffector2D>().density = 1f;
                liquid.tag = "Water";
                UI_selectedLiquid.text = "Víz (1 g/cm" + (char)0x00B3 + ")";
                break;
            case 1:
                liquid.GetComponent<SpriteRenderer>().color = oilColor;
                liquid.GetComponent<BuoyancyEffector2D>().density = .9f;
                liquid.tag = "Oil";
                UI_selectedLiquid.text = "Olaj (0.9 g/cm" + (char)0x00B3 + ")";
                break;
            default:
                Debug.Log("Invalid index");
                break;
        }
    }
}
