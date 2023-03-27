using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Slider jumpSlider;
    public TextMeshProUGUI yVelocityTMP;
    private enum MovementState { Idle, Running, Jumping, Falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        yVelocityTMP.text = "Függőleges irányú sebesség: " + rb.velocity.y.ToString("F2") + " m/s";
        horizontalInput = Input.GetAxisRaw("Horizontal");
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalInput * 5f, rb.velocity.y);
        SetAppropriateAnimation();
        FlipSprite();
        Jump();
    }

    private void SetAppropriateAnimation()
    {
        MovementState state;
        
        if (horizontalInput > 0f && isGrounded == true)
        {
            state = MovementState.Running;
        }
        else if (horizontalInput < 0f && isGrounded == true)
        {
            state = MovementState.Running;
        }
        else if (horizontalInput == 0f && isGrounded == true)
        {
            state = MovementState.Idle;
        }
        else
        {
            state = MovementState.Falling;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.Falling;
        }
        
        animator.SetInteger("state", (int)state);
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float jumpForce = jumpSlider.value;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void FlipSprite()
    {
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
