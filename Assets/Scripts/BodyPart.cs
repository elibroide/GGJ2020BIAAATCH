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
    public BodyPartController bodyPartController;

    public BodyPartData CreateData(string owner)
    {
        return new BodyPartData
        {
            parent = this,
            @group = @group,
            health = this.health,
            decompositionRate = this.decompositionRate,
            ownerName = owner,
            type = this.type,
        };
    }
}

[Serializable]
public class BodyPartData
{
    public string @group;
    public float health;
    public float decompositionRate;
    public string ownerName;
    public BodyPartType type;
    public BodyPart parent;

    public void Decompose(float deltaTime)
    {
        health -= deltaTime;
    }
}
