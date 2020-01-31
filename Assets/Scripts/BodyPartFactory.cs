using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodyPartFactory : MonoBehaviour
{
    public static BodyPartFactory Instance;
    
    public Transform itemsParent;
    
    public string[] names;
    public BodyPart[] parts;
    private string[] _groups;

    private void Awake()
    {
        Instance = this;
        parts = Resources.LoadAll<BodyPart>("/");
        _groups = parts.Select(part => part.@group).Distinct().ToArray();
    }

    public BodyPart[] GetBodyPartOfGroup(string type)
    {
        return parts.Where(part => part.group == type).ToArray();
    }
    
    public BodyPart GetBodyPart()
    {
        return parts[Random.Range(0, parts.Length)];
    }

    public string GetName()
    {
        return names[Random.Range(0, names.Length)];
    }

    public BodyPartPickup CreatePickup()
    {
        var part = GetBodyPart();
        var pickup = Instantiate(part.bodyPartPickup).GetComponent<BodyPartPickup>();
        pickup.data = part.CreateData(GetName());
        return pickup;
    }
}
