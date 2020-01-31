using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    public Transform target;
    private Vector3 lastPos;
    public float speed = 2;
    public float distance = 1;

    private void Start()
    {
        camera = GetComponent<Camera>();
        lastPos = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 diff = target.position - lastPos;
        Vector3 targetPos = target.position;
        if (diff.magnitude > 0 )
        {
            // moving
            targetPos += diff * distance;
        }
        

        transform.DOMoveX(targetPos.x, speed);
        transform.DOMoveY(targetPos.y, speed);
        lastPos = target.position;
    }

    public void FocusIn()
    {
        camera.DOOrthoSize(4, 1);
    }

    public void FocusOut()
    {
        camera.DOOrthoSize(5, 1);
    }

}
