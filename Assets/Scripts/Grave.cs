using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Grave : MonoBehaviour
{
    public int dropItems = 1;
    public float health = 2;
    [FormerlySerializedAs("isDead")] public bool isOpened = false;

    public SpriteRenderer closed;
    public SpriteRenderer opened;
    public Transform[] drops;
    public ParticleSystem particles;

    public Transform bubbleParent;

    public BodyPartPickup hiddenItem;
    public Bubble bubble;
    public BodyPartData _bodyPart;
    private float _startingHealth;

    void Start()
    {
        _bodyPart = BodyPartFactory.Instance.GetBodyPartData();
        _startingHealth = health;
    }

    public void Peek()
    {
        if (bubble != null)
        {
            return;
        }
        bubble = BodyPartFactory.Instance.CreateBubble(_bodyPart);
        bubble.transform.SetParent(bubbleParent, true);
        bubble.transform.localPosition = Vector3.zero;
        bubble.Show();
    }

    public void HidePeek()
    {
        if (bubble != null)
        {
            bubble.Hide();
        }
        bubble = null;
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        // particles.Emit(Mathf.CeilToInt(1 + Random.value * 3));
        if (health <= 0) Kill();
    }

    private void Kill()
    {
        isOpened = true;
        closed.gameObject.SetActive(false);
        opened.gameObject.SetActive(true);

        hiddenItem = BodyPartFactory.Instance.CreatePickup(_bodyPart);
        hiddenItem.transform.position = drops[0].position;

        DOVirtual.DelayedCall(45, Refill);
    }

    private void Refill()
    {
        _bodyPart = BodyPartFactory.Instance.GetBodyPartData();
        isOpened = false;
        health = _startingHealth;
        closed.gameObject.SetActive(true);
        opened.gameObject.SetActive(false);
    }
}
