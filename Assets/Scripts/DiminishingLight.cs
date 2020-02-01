using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DiminishingLight : MonoBehaviour
{

    public Transform target;
    private float smallest = 0.5f;
    private float largest = 10;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position;
    }

    public void UpdateSize(float percent)
    {
        transform.localScale = Vector3.Lerp(Vector3.one * smallest, Vector3.one * largest, percent);
    }
}
