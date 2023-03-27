using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isDragged;
    public bool isCollidingWithLiquid = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDragged = true;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (isDragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }

        //Might need to change this a little --> for code optimization
        if (!isDragged && transform.position.y < -7f) Destroy(gameObject);
    }

    private void OnMouseDown()
    {   

        rb.velocity = Vector3.zero;
        if (isDragged)
        {
            isDragged = false;
            rb.isKinematic = false;
            rb.freezeRotation = false;
        }
        else if (!isDragged)
        {
            if (!isCollidingWithLiquid)
            {
                isDragged = true;
                rb.isKinematic = true;
                rb.freezeRotation = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" || collision.gameObject.tag == "Oil")
        {
            if (!isDragged)
            {
                isCollidingWithLiquid = true;
            }
        }
    }
}
