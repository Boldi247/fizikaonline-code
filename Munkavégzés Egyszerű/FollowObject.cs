using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject objToFollow;
    public GameObject startPoint;
    public GameObject distanceTextContainer;
    private TextMeshPro distanceText;
    public float centerOffset = .8f;
    
    private void Awake()
    {
        distanceText = distanceTextContainer.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position = objToFollow.transform.position;
        float distanceY = objToFollow.transform.position.y - startPoint.transform.position.y;
        transform.localScale = new Vector3(1, distanceY, 1);
        distanceText.text = (distanceY - centerOffset).ToString("F1") + " m";
    }
}
