using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public int dropItems = 1;
    public float health = 2;
    public bool isDead = false;

    public Transform[] drops;

    public void TakeHit(float damage)
    {
        health -= damage;
        
        if (health <= 0) Kill();
    }

    private void Kill()
    {
        isDead = true;
        Destroy(gameObject);

        var itemsDropped = 0;
        foreach (var drop in drops)
        {
            if (itemsDropped == dropItems)
            {
                break;
            }
            var pickup = BodyPartFactory.Instance.CreatePickup();
            pickup.transform.position = drop.position;
            itemsDropped++;
        }
    }
}
