using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
[CreateAssetMenu(fileName = "BodyPart", menuName = "BodyPart", order = 1)]
public class BodyPart : ScriptableObject
{
    public string @group;
    public float health;
    public float decompositionRate;
    public BodyPartType type;
    public GameObject front;
    public GameObject back;
    public GameObject left;
    public GameObject right;
    public GameObject dig;
    public GameObject bodyPartPickup;

    public BodyPartData CreateData(string owner)
    {
        var decomposeRate = 1 + UnityEngine.Random.value * 4;
        return new BodyPartData
        {
            decomposeRate = decomposeRate,
            parent = this,
            health = this.health,
            ownerName = owner,
            type = this.type,
        };
    }
}

[Serializable]
public class BodyPartData
{
    public float health;
    public string ownerName;
    public BodyPartType type;
    public BodyPart parent;
    public float decomposeRate { get; set; }

    public void Decompose(float deltaTime)
    {
        health -= deltaTime * decomposeRate;
    }
}
