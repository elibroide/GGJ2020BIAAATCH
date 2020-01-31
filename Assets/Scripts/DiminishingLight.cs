using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DiminishingLight : MonoBehaviour
{

    public Transform target;
    // Use this for initialization
    void Start()
    {
        transform.DOScale(0.5f,10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position;
    }
}
