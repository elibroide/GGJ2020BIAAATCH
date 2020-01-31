using System;
using DG.Tweening;
using UnityEngine;


public class DiggingScreen : MonoBehaviour
{
    public Transform left;
    public Transform right;

    public float timeToAppear = 0.5f;
    
    public float minScale = 0.7f;
    public float maxScale = 1.0f;
    public float timeToScale = 0.5f;

    private Tween _tween;
    private Vector3 _returnPosition;
    private string _tweenId = "digScreen";

    public void Start()
    {
        _returnPosition = transform.localPosition;
    }

    public void Show()
    {
        left.localScale = Vector3.one * minScale;
        right.localScale = Vector3.one * maxScale;
        var sequence = DOTween.Sequence();
        sequence.Append(((RectTransform)transform).DOAnchorPos(Vector3.zero, timeToAppear).SetEase(Ease.OutBack));
        var duration = sequence.Duration();
        left.DOScale(Vector3.one * maxScale, timeToScale).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetId(_tweenId);
        right.DOScale(Vector3.one * minScale, timeToScale).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetId(_tweenId);
        _tween = sequence;
    }

    public void Hide()
    {
        _tween?.Kill();
        DOTween.Kill(_tweenId);
        left.localScale = Vector3.one * minScale;
        right.localScale = Vector3.one * maxScale;
        transform.DOLocalMove(_returnPosition, timeToAppear);
    }
}
