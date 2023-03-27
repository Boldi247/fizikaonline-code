using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowFacing : MonoBehaviour
{
    public float rotateSpeed = 5f;

    RotateAroundObject rotateAroundObject;

    private void Awake()
    {
        rotateAroundObject = GetComponentInParent<RotateAroundObject>();
    }

    private void FixedUpdate()
    {
        Vector2 velDir = rotateAroundObject.GetTangentVelocityDirection();
        float angle = Mathf.Atan2(velDir.y, velDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed * Time.deltaTime);
    }
}
