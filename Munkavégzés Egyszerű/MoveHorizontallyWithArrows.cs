using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontallyWithArrows : MonoBehaviour
{
    private float movementDistance = 1f;
    public GameObject ball;

    public void LeftArrowPressed()
    {
        if (ball.transform.position.x > -6) ball.transform.position = new Vector3(ball.transform.position.x - movementDistance, ball.transform.position.y, ball.transform.position.z);
    }

    public void RightArrowPressed()
    {
        if (ball.transform.position.x < 6) ball.transform.position = new Vector3(ball.transform.position.x + movementDistance, ball.transform.position.y, ball.transform.position.z);
    }

    public void OnResetButtonPressed()
    {
        ball.transform.position = new Vector3(0, 0, 0);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
