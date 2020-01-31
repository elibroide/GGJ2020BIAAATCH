using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodyPartFactory : MonoBehaviour
{
    public string[] names;
    public BodyPart[] parts;
    private string[] _groups;

    private void Awake()
    {
        parts = Resources.LoadAll<BodyPart>("/");
        _groups = parts.Select(part => part.@group).Distinct().ToArray();
    }

    public BodyPart[] GetBodyPartOfGroup(string type)
    {
        return parts.Where(part => part.group == type).ToArray();
    }
    
    public BodyPart GetBodyPart(string ownerName)
    {
        return parts[Random.Range(0, parts.Length)];
    }

    public string GetName(string ownerName)
    {
        return (ownerName == "") ? names[Random.Range(0, names.Length)] : ownerName;
    }
}
