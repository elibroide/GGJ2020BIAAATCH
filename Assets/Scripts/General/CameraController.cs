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
    public float zoomIn = 4;
    public float zoomOut = 5;
    public float focusInDuration = 1;
    public float focusOutDuration = 1;

    private Tween _tween;

    private void Start()
    {
        camera = GetComponent<Camera>();
        lastPos = target.position;
        camera.orthographicSize = zoomOut;
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

        _tween?.Kill();
        _tween = transform.DOMove(new Vector3(targetPos.x, targetPos.y, transform.position.z), speed);
        lastPos = target.position;
    }

    public void FocusIn()
    {
        camera.DOOrthoSize(zoomIn, focusInDuration).SetEase(Ease.OutBack);
    }

    public void FocusOut()
    {
        camera.DOOrthoSize(zoomOut, focusOutDuration).SetEase(Ease.OutBack);
    }

    public void Shake(float duration, float strength)
    {
        transform.DOShakePosition(duration, Vector3.one * strength);
    }

}
