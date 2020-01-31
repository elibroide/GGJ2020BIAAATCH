using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grave : MonoBehaviour
{
    public int dropItems = 1;
    public float health = 2;
    public bool isDead = false;

    public BodyPartPickup hiddenItem;
    public SpriteRenderer closed;
    public SpriteRenderer opened;
    public Transform[] drops;
    public ParticleSystem particles;

    void Start()
    {
        hiddenItem = BodyPartFactory.Instance.CreatePickup();
        hiddenItem.gameObject.SetActive(false);
        foreach (var drop in drops)
        {
            hiddenItem.transform.position = drop.position;
        }
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        particles.Emit(Mathf.CeilToInt(1 + Random.value * 3));
        if (health <= 0) Kill();
    }

    private void Kill()
    {
        isDead = true;
        closed.gameObject.SetActive(false);
        opened.gameObject.SetActive(true);

        hiddenItem.gameObject.SetActive(true);
    }
}
