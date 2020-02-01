using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodyPartFactory : MonoBehaviour
{
    public static BodyPartFactory Instance;

    public BodyPartPickup pickupPrefab;
    public Bubble bubblePrefab;
    public Transform itemsParent;
    
    [ReadOnly]
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
    
    public BodyPartData GetBodyPartData()
    {
        return parts[Random.Range(0, parts.Length)].CreateData(GetName());
    }

    public string GetName()
    {
        return names[Random.Range(0, names.Length)];
    }

    public BodyPartPickup CreatePickup(BodyPartData data)
    {
        var pickup = Instantiate(pickupPrefab);
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
