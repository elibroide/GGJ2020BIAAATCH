using DG.Tweening;
using UnityEngine;

public class FlashBolt : MonoBehaviour
{
    public SpriteRenderer flash;
    public float timeToFlash;
    public float timeBetweenFlash;
    public float timeFromFlash;

    private Tween _tween;

    public void Awake()
    {
        flash.color = Color.clear; 
    }

    public Tween DoFlash()
    {
        _tween?.Kill();
        var sequence = DOTween.Sequence();
        sequence.Append(flash.DOColor(Color.white, timeToFlash).SetEase(Ease.OutCirc));
        sequence.AppendInterval(timeBetweenFlash);
        sequence.Append(flash.DOColor(Color.clear, timeFromFlash).SetEase(Ease.InCirc));
        _tween = sequence; 
        return sequence;
    }
}