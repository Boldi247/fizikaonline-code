using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSound : MonoBehaviour
{
    private MoveVertically moveVertically;
    public AudioSource audioSource;
    [SerializeField] float maxDist;

    private void Start()
    {
        moveVertically = gameObject.GetComponent<MoveVertically>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float dist = Mathf.Abs(moveVertically.GetMaxDraggedY() + 5f - .8f);
        Debug.Log(dist);
        if (dist >= maxDist)
        {
            audioSource.volume = 1;
            audioSource.Play();
        }
        else
        {
            float ratio = dist / maxDist;
            audioSource.volume = ratio;
            audioSource.Play();
        }
    }
}
