using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{  
    private float speed = 6f;
    private SpriteRenderer spriteRenderer;
    private bool canMove = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        if (canMove) HandleMovement(horizontalMovement, verticalMovement);
    }
    
    private void HandleMovement(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement * speed * Time.deltaTime;

        HandleBounds();
        FlipSprite(horizontal);
    }

    private void HandleBounds()
    {
        if (transform.position.x <= -9.65f)
        {
            transform.position = new Vector3(9.65f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 9.65f)
        {
            transform.position = new Vector3(-9.65f, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= 4.8f)
        {
            transform.position = new Vector3(transform.position.x, 4.8f, transform.position.z);
        }

        else if (transform.position.y <= -4.8f)
        {
            transform.position = new Vector3(transform.position.x, -4.8f, transform.position.z);
        }
    }

    private void FlipSprite(float horizontal)
    {
        if (horizontal > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
