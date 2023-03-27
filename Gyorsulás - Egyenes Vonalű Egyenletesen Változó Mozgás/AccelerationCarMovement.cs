using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationCarMovement : MonoBehaviour
{
    public Slider accelerationSlider;
    private float speed = 0;
    private float horizontalInput;
    private SpriteRenderer spriteRenderer;

    private Vector3 originalPosition;

    public TextMeshProUGUI speedText;

    public GameObject background;
    private Vector3 bgOgPos;

    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        bgOgPos = background.transform.position;
    }

    void Update()
    {
        FlipCar();
        CarMovement();
        UpdateDisplayValues();
        PlaceBackIfOutOfBounds();
    }

    private void UpdateDisplayValues()
    {
        speedText.text = Mathf.Abs(speed).ToString("F2") + " m/s (" + Mathf.Abs((float)(speed * 3.6)).ToString("F2") + " km/h)";
    }

    private void PlaceBackIfOutOfBounds()
    {
        if (transform.position.x < -10 || transform.position.x > 10)
        {
            transform.position = originalPosition;
            speed = 0;
            spriteRenderer.flipX = true;
            background.transform.position = bgOgPos;
        }
    }

    private void FlipCar()
    {
        if (horizontalInput >= 0 && speed > -0.2f)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput < 0 && speed < 0.2f)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void CarMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        speed += horizontalInput * accelerationSlider.value * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        
        if (transform.position.x > -6f && transform.position.x < 3.6f) background.transform.position =
            new Vector3(background.transform.position.x - speed * Time.deltaTime,
            background.transform.position.y, background.transform.position.z);
    }
}
