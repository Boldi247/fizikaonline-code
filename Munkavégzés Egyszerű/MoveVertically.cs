using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveVertically : MonoBehaviour
{
    private float startPosY;
    private bool isDragged = false;
    private Rigidbody2D rb;
    private float maxDraggedY;

    public GameObject scaleBottom;
    public GameObject textParent;
    private TextMeshPro textComp;
    
    private bool grounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        textComp = textParent.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((mousePos.y - startPosY) > maxDraggedY)
            {
                maxDraggedY = mousePos.y - startPosY;
                transform.position = new Vector3(transform.position.x, mousePos.y - startPosY, 0);
            }
        }
        float dist = Mathf.Round((transform.position.y - scaleBottom.transform.position.y - .8f) * 10f) / 10f;
        float force = Mathf.Round((rb.mass * 10) * 10f) / 10f;
        textComp.text = (dist * force).ToString("F1") + " J";
    }

    public float GetMaxDraggedY()
    {
        return maxDraggedY;
    }
    
    private void OnMouseDown()
    {
        if (grounded)
        {
            isDragged = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosY = mousePos.y - transform.position.y;
            maxDraggedY = mousePos.y - startPosY;
            rb.bodyType = RigidbodyType2D.Static;
            textParent.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        textParent.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }

   private void OnCollisionExit2D(Collision2D collision)
   {
        grounded = false;
   }
}
