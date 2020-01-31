using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodyPartFactory : MonoBehaviour
{
    public static BodyPartFactory Instance;
    
    public Transform itemsParent;
    
    public string[] names;

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
