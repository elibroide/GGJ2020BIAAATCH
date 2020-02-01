using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Text text;

    public Sprite head;
    public Sprite handLeft;
    public Sprite handRight;
    public Sprite legLeft;
    public Sprite legRight;
    
    public SpriteRenderer partImage;

    public float moveUp = 5;
    public float moveUpDuration = 0.2f;
    public float moveDownDuration = 0.2f;
    public float hoverUp = 0.25f;
    public float hoverUpDuration = 0.75f;

    private Tween _tween;

    public void Init(BodyPartData data)
    {
        text.text = $"{data.ownerName}";
        transform.localScale = Vector3.zero;
        switch (data.type)
        {
            case BodyPartType.Head:
                partImage.sprite = head; 
                break;
            case BodyPartType.HandLeft:
                partImage.sprite = handLeft; 
                break;
            case BodyPartType.HandRight:
                partImage.sprite = handRight; 
                break;
            case BodyPartType.LegLeft:
                partImage.sprite = legLeft; 
                break;
            case BodyPartType.LegRight:
                partImage.sprite = legRight; 
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Show()
    {
        // spriteRenderer.sprite = sprite;
        _tween?.Kill();
        var sequence = DOTween.Sequence().OnStart(()=>gameObject.SetActive(true));
        sequence.Insert(0, transform.DOLocalMove(Vector3.up * moveUp, moveUpDuration));
        sequence.Insert(0, transform.DOScale(Vector3.one, moveUpDuration));
        sequence.AppendCallback(() =>
            {
                _tween = transform.DOLocalMove(Vector3.up * moveUp + Vector3.up * hoverUp, hoverUpDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            });
        _tween = sequence;
    }

    public void Hide()
    {
        _tween?.Kill();
        var sequence = DOTween.Sequence().OnComplete(()=>gameObject.SetActive(false));
        sequence.Insert(0, transform.DOLocalMove(Vector3.zero, moveDownDuration));
        sequence.Insert(0, transform.DOScale(Vector3.zero, moveDownDuration));
        sequence.AppendCallback(() => Destroy(gameObject));
        _tween = sequence;
    }
}
