using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{

    public float health = 2;

    public bool isDead = false;
       // Start is called before the first frame update
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
        Debug.Log("DEAD");
        Destroy(gameObject);
        GameObject a = Resources.Load("grave") as GameObject;
        a.name = Time.time.ToString();
        Instantiate(a);

    }
}
