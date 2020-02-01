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
        return new BodyPartData
        {
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

    public void Decompose(float deltaTime)
    {
        var decomposeRate = 1;
        switch (type)
        {
            case BodyPartType.Head:
                decomposeRate = 3;
                break;
            case BodyPartType.HandLeft:
            case BodyPartType.HandRight:
                decomposeRate = 1;
                break;
            case BodyPartType.LegLeft:
            case BodyPartType.LegRight:
                decomposeRate = 2;
                break;
        }
        health -= deltaTime * decomposeRate;
    }
}
