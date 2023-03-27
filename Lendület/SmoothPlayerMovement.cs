using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class SmoothPlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    public float speed = 10f;
    private bool playerInAir = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public AudioSource jumpSound;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI speedAtTheMomentOfJumpText;
    public TextMeshProUGUI momentumText;
    public TextMeshProUGUI momemntumUI;

    private enum PlayerState
    {
        Idle,
        Walking,
        Jumping
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        speedText.text = Mathf.Abs(rb.velocity.x).ToString("F1") + " m/s (" + 
            (Mathf.Abs(rb.velocity.x) * 3.6f).ToString("F0") + " km/h)";
        
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleHorizontalMovement();
    }

    private void HandleHorizontalMovement()
    {
        if (!playerInAir) // if the player is not in the air, then move
        {
            rb.AddForce(Vector2.right * horizontalInput * speed);

            if (horizontalInput > 0)
            {
                sr.flipX = false;
            }
            else if (horizontalInput < 0)
            {
                sr.flipX = true;
            }

            if (horizontalInput != 0)
            {
                SetAppropriateAnimationState(PlayerState.Walking);
            }
            else
            {
                SetAppropriateAnimationState(PlayerState.Idle);
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerInAir)
        {
            rb.AddForce(Vector2.up * 120f, ForceMode2D.Impulse);
            jumpSound.Play();

            speedAtTheMomentOfJumpText.text = Mathf.Abs(rb.velocity.x).ToString("F1") + " m/s (" + 
                (Mathf.Abs(rb.velocity.x) * 3.6f).ToString("F0") + " km/h)";
            momentumText.text = CalculateMomentum().ToString("F0") + " kg m/s";
            momemntumUI.text = CalculateMomentum().ToString("F0") + " kg m/s = " + rb.mass + " kg * " + Mathf.Abs(rb.velocity.x).ToString("F1") + " m/s";
        }
    }

    private float CalculateMomentum()
    {
        return (float)Math.Round(Mathf.Abs(rb.velocity.x), 1) * rb.mass;
    }

    private void SetAppropriateAnimationState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                anim.SetInteger("state", 0);
                break;
            case PlayerState.Walking:
                anim.SetInteger("state", 1);
                break;
            case PlayerState.Jumping:
                anim.SetInteger("state", 0);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerInAir = false;

            speedAtTheMomentOfJumpText.text = "0 m/s (0 km/h)";
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SetAppropriateAnimationState(PlayerState.Jumping);
            playerInAir = true;
        }
    }
}