using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodyPartFactory : MonoBehaviour
{
    public static BodyPartFactory Instance;

    public BodyPartPickup pickupPrefab;
    public Bubble bubblePrefab;
    public Transform itemsParent;
    
    
    public string[] names = new string[]
    {
"Rogelio",
"In",
"Dara",
"Diana",
"Lamonica",
"Jeneva",
"Korey",
"Felicidad",
"Hye",
"Ilda",
"Jolene",
"Valeri",
"Rosella",
"Les",
"Lauri",
"Carma",
"Leonore",
"Alona",
"Ada",
"Lai",
"Jill",
"Galev",
"Erwin",
"Wilhelmina",
"Aaron",
"Broderick",
"Bettina",
"Hilario",
"Kingv",
"Zana",
"Loriav",
"Winonav",
"Crystav",
"Mathew",
"Verdell",
"Ora",
"Grant",
"Lyndsey",
"Lorriane",
"Kristyn",
"Lynelle",
"Cherrie",
"Ardelia",
"Curt",
"Nicolas",
"Rolando",
"Frederick",
"Annice",
"Annalisa",
"Ruthann",
    };

    public BodyPart[] parts;
    private string[] _groups;
    private Queue<BodyPart> _bucket = new Queue<BodyPart>();

    private void Awake()
    {
        Instance = this;
        parts = Resources.LoadAll<BodyPart>("/");
        _groups = parts.Select(part => part.@group).Distinct().ToArray();
        
        _bucket = new Queue<BodyPart>();
    }

    private void FillBucket()
    {
        var list = new List<BodyPart>();
        var indexes = new List<int>();
        foreach (var part in parts)
        {
            var amount = part.type == BodyPartType.Body ? 1 : 5;
            for (var i = 0; i < amount; i++)
            {
                list.Add(part);
                indexes.Add(indexes.Count);
            }
        }
        while (indexes.Count > 0)
        {
            var index = indexes[UnityEngine.Random.Range(0, indexes.Count)];
            _bucket.Enqueue(list[index]);
            indexes.Remove(index);
        }
    }

    public string GetGroup()
    {
        return _groups[UnityEngine.Random.Range(0, _groups.Length)];
    }

    public BodyPart[] GetBodyPartOfGroup(string type)
    {
        return parts.Where(part => part.group == type).ToArray();
    }
    
    public BodyPart GetBodyPart()
    {
        if (_bucket.Count == 0)
        {
            FillBucket();
        }
        return _bucket.Dequeue();
    }
    
    public BodyPartData GetBodyPartData()
    {
        return GetBodyPart().CreateData(GetName());
    }

    public string GetName()
    {
        return names[Random.Range(0, names.Length)];
    }

    public BodyPartPickup CreatePickup(BodyPartData data)
    {
        var pickupObject = Instantiate(data.parent.bodyPartPickup);
        var pickup = Instantiate(pickupPrefab);
        pickupObject.transform.SetParent(pickup.transform, false);
        pickupObject.transform.localPosition = Vector3.zero;
        pickup.Init(data);
        return pickup;
    }

    public Bubble CreateBubble(BodyPartData data)
    {
        var bubble = Instantiate(bubblePrefab);
        bubble.Init(data);
        return bubble;
    }
}
