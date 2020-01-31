using System;
using UnityEngine;

public class BodyPart
{
    public float health { get; private set; }
    public float decompositionRate { get; private set; }
    public string ownerName { get; private set; }
    public BodyPartType type { get; private set; }

    public BodyPart(BodyPartType type,float initial_health = 1, string initialOwnerName="Me")
    {
        ownerName = initialOwnerName;
        health = initial_health;
    }

    public void Decompose(float deltaTime)
    {
        health -= deltaTime;
    }

    
}
