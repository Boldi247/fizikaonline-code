using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDragNoRB : MonoBehaviour
{
    private float startPosY;
    private bool isDragged = false;

    public GameObject water;

    private void Update()
    {
        if (isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (CheckIfAboveMaxDepth(mousePos) && CheckIfDraggingDownwards(mousePos))
            {
                transform.position = new Vector3(transform.position.x, mousePos.y - startPosY, 0);
            }
        }
    }

    private bool CheckIfDraggingDownwards(Vector3 mousePos)
    {
        if (mousePos.y - startPosY < transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfAboveMaxDepth(Vector3 mousePos)
    {
        if (transform.position.y + transform.localScale.y/2 < water.transform.position.y + water.transform.localScale.y/2)
        {
            return false;
        }
        else return true;
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosY = mousePos.y - transform.position.y;
        isDragged = true;
    }

    private void OnMouseUp()
    {
        isDragged = false;
    }

    public bool GetDragged()
    {
        return isDragged;
    }
}
