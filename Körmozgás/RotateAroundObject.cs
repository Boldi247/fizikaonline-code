using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateAroundObject : MonoBehaviour
{
    public GameObject center;

    private Rigidbody2D rb;
    private Transform _transform;
    private float _radius;

    DistanceJoint2D distanceJoint2D;
    private bool movementEnabled;

    Vector2 tangentVelocityDirection;

    public bool bool_rotateAroundItself = true;

    public Slider speedSlider;
    
    private void Awake()
    {
        movementEnabled = true;
        rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _radius = GetDistance(center.transform.position);
        AddDistanceJoint2D();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            distanceJoint2D.enabled = false;
            movementEnabled = false;
            StartCoroutine(RestartScene());
        }

        if (movementEnabled)
        {
            Vector2 centripetalForceDirection = (center.transform.position - _transform.position).normalized;
            tangentVelocityDirection = GetPerPendicularDirection(centripetalForceDirection);
            rb.velocity = tangentVelocityDirection * speedSlider.value;
        }

        if (bool_rotateAroundItself) RotateAroundItself();
    }

    private void RotateAroundItself()
    {
        float angle = Mathf.Atan2(tangentVelocityDirection.y, tangentVelocityDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), speedSlider.value * Time.deltaTime);
    }

    private Vector2 GetPerPendicularDirection(Vector2 v)
    {
        return (new Vector2(v.y, -v.x)).normalized;
    }
    
    private void AddDistanceJoint2D()
    {
        distanceJoint2D = gameObject.AddComponent<DistanceJoint2D>();
        distanceJoint2D.connectedBody = center.GetComponent<Rigidbody2D>();
        distanceJoint2D.autoConfigureDistance = true;
        distanceJoint2D.maxDistanceOnly = true;
    }
    
    private float GetDistance(Vector2 center)
    {
        float x_d = center.x - _transform.position.x;
        float y_d = center.y - _transform.position.y;
        return (float) Mathf.Sqrt(x_d * x_d + y_d * y_d);
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public Vector2 GetTangentVelocityDirection()
    {
        return tangentVelocityDirection;
    }
}