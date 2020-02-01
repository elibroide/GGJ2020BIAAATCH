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
        Debug.Log(percent);
        float scaleFactor = percent * (9.5f) + 0.5f;
        Debug.Log("-----"+scaleFactor);
        transform.DOScale(Vector3.one * scaleFactor,0.5f);
    }
}
