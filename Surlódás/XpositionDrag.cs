using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*A script to drag the box, only on the x axis
Handles the color of the side arrows of the box, which display which direction is
the box being dragged at. It displays the friction force, and the force needed to overcome it.
It implements an offset which size is depending on the weight of the box. The heavier the box is,
larger the offset is.
Once the box is dragged in a direction, it can't be dragged in the opposite direction.

When the box is no longer dragged, it repositions to 0 on the x axis*/
public class XpositionDrag : MonoBehaviour
{
    private Vector3 mousePosOffset;
    private Vector3 dragStartPosition;
    public float dragOffsetDistance;

    public GameObject rightArrowRect;
    public GameObject leftArrowRect;
    public GameObject rightArrowTriangle;
    public GameObject leftArrowTriangle;

    private Color greenArrowColor;
    private Color redArrowColor;
    private Color greyArrowColor;

    private bool movedLeft;
    private bool movedRight;
    private float maxUnitsMoved;

    public Slider frictionSlider;
    public Slider weightSlider;
    public GameObject rightArrowTextContainer;
    public GameObject leftArrowTextContainer;
    private TextMeshPro rightArrT;
    private TextMeshPro leftArrT;
    public TextMeshPro rightArrFType;
    public TextMeshPro leftArrFType;
    private float frictionForce;
    private bool startedMoving;
    
    private void Awake()
    {
        UnityEngine.ColorUtility.TryParseHtmlString("#01802B", out greenArrowColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#A40000", out redArrowColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#808080", out greyArrowColor);

        rightArrT = rightArrowTextContainer.GetComponent<TextMeshPro>();
        leftArrT = leftArrowTextContainer.GetComponent<TextMeshPro>();

        ArrowsEnabled(false);
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void OnMouseDown()
    {
        startedMoving = false;
        maxUnitsMoved = 0f;
        dragStartPosition = GetMouseWorldPosition();
        mousePosOffset = gameObject.transform.position - dragStartPosition;
    }

    private void OnMouseUp()
    {
        rightArrT.text = "0 N";
        leftArrT.text = "0 N";

        movedLeft = false;
        movedRight = false;

        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        ArrowsEnabled(false);
    }

    private void OnMouseDrag()
    {
        ArrowsEnabled(true);

        float unitsMoved = GetMouseWorldPosition().x - dragStartPosition.x;

        if (Math.Sign(unitsMoved) == 1 && movedLeft == false) //if moved right
        {
            movedRight = true;

            rightArrowRect.GetComponent<SpriteRenderer>().color = greenArrowColor;
            rightArrowTriangle.GetComponent<SpriteRenderer>().color = greenArrowColor;
            leftArrowRect.GetComponent<SpriteRenderer>().color = redArrowColor;
            leftArrowTriangle.GetComponent<SpriteRenderer>().color = redArrowColor;

            leftArrFType.text = "F(s)";
            rightArrFType.text = "F(h)";

            frictionForce = (frictionSlider.value * weightSlider.value * 10f);
            leftArrT.text = frictionForce.ToString("F0") + " N";
            CalculateDragOffsetDistance();
            if (!startedMoving) rightArrT.text = (unitsMoved*30).ToString("F0") + " N";
            
            if (unitsMoved >= dragOffsetDistance && unitsMoved > maxUnitsMoved)
            {
                rightArrT.text = "> " + frictionForce.ToString("F0") + " N";
                startedMoving = true;
                maxUnitsMoved = unitsMoved;
                transform.position = new Vector3(GetMouseWorldPosition().x + mousePosOffset.x - dragOffsetDistance,
                    transform.position.y, transform.position.z);
            }
        }
        else if (Math.Sign(unitsMoved) == -1 && movedRight == false) // if moved left
        {
            movedLeft = true;

            rightArrowRect.GetComponent<SpriteRenderer>().color = redArrowColor;
            rightArrowTriangle.GetComponent<SpriteRenderer>().color = redArrowColor;
            leftArrowRect.GetComponent<SpriteRenderer>().color = greenArrowColor;
            leftArrowTriangle.GetComponent<SpriteRenderer>().color = greenArrowColor;

            leftArrFType.text = "F(h)";
            rightArrFType.text = "F(s)";

            frictionForce = (frictionSlider.value * weightSlider.value * 10f);
            rightArrT.text = frictionForce.ToString("F0") + " N";
            CalculateDragOffsetDistance();
            if (!startedMoving) leftArrT.text = Mathf.Abs(unitsMoved * 30).ToString("F0") + " N";

            if (unitsMoved <= -dragOffsetDistance && unitsMoved < maxUnitsMoved)
            {
                leftArrT.text = "> " + Mathf.Abs(frictionForce).ToString("F0") + " N";
                startedMoving = true;
                maxUnitsMoved = unitsMoved;
                transform.position = new Vector3(GetMouseWorldPosition().x + mousePosOffset.x + dragOffsetDistance, transform.position.y, transform.position.z);
            }
        }
        else if (Math.Sign(unitsMoved) == 0 && movedRight == false && movedLeft == false)
        {
            rightArrowRect.GetComponent<SpriteRenderer>().color = greyArrowColor;
            rightArrowTriangle.GetComponent<SpriteRenderer>().color = greyArrowColor;
            leftArrowRect.GetComponent<SpriteRenderer>().color = greyArrowColor;
            leftArrowTriangle.GetComponent<SpriteRenderer>().color = greyArrowColor;

            leftArrFType.text = "";
            rightArrFType.text = "";
        }
    }

    private void CalculateDragOffsetDistance()
    {
        dragOffsetDistance = frictionForce / 30f;
    }
    
    private void ArrowsEnabled(bool enabled)
    {
        if (enabled)
        {
            rightArrowRect.SetActive(true);
            leftArrowRect.SetActive(true);
            rightArrowTriangle.SetActive(true);
            leftArrowTriangle.SetActive(true);
        }
        else
        {
            rightArrowRect.SetActive(false);
            leftArrowRect.SetActive(false);
            rightArrowTriangle.SetActive(false);
            leftArrowTriangle.SetActive(false);
        }
    }
}
