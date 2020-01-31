using System;
using UnityEngine;

public class BodyPartController:MonoBehaviour
{
    [ReadOnly]
    public float health;
    [ReadOnly]
    public float decompositionRate;
    [ReadOnly]
    public string ownerName;
    [ReadOnly]
    public BodyPartType type;

    public BodyPartController(BodyPartType type,float initial_health = 1, string initialOwnerName="Me")
    {
        ownerName = initialOwnerName;
        health = initial_health;
    }

    public void Decompose(float deltaTime)
    {
        health -= deltaTime;
    }

    
}
