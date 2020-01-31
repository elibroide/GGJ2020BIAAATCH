using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public float health = 2;
    public bool isDead = false;

    void Start()
    {
        
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        
        if (health <= 0) Kill();
    }

    private void Kill()
    {
        isDead = true;
        Destroy(gameObject);
        // GameObject a = ResourceManager.GetGameObject("grave");
        // a.name = Time.time.ToString();
        // Instantiate(a);

    }
}
