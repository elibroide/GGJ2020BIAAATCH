using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class BodyParts : MonoBehaviour
{
    Dictionary<BodyPartType, BodyPart> body = new Dictionary<BodyPartType, BodyPart>();
    Action<List<BodyPart>> onDropParts;

    // Start is called before the first frame update
    void Start()
    {
        body[BodyPartType.Head] = new BodyPart(BodyPartType.Head,1, "head");
        body[BodyPartType.HandRight]= new BodyPart(BodyPartType.HandRight, 2,"hr");
        body[BodyPartType.HandLeft] = new BodyPart(BodyPartType.HandLeft, 3, "hl");
        body[BodyPartType.LegLeft] = new BodyPart(BodyPartType.LegLeft, 2, "ll");
        body[BodyPartType.LegRight] = new BodyPart(BodyPartType.LegRight, 3, "lr");
    }

    private void Update()
    {
        OnTick();
    }

    public void OnTick()
    {
        
        // decompose all
        float delta = Time.deltaTime;
        
        List<BodyPartType> deadParts = new List<BodyPartType>();

        foreach (KeyValuePair<BodyPartType, BodyPart>  entry in body)
        {
            BodyPart part = entry.Value;
            if (part != null)
            {
                part.Decompose(delta);
                if (part.health <= 0) deadParts.Add(entry.Key);
            }
        }

        if (deadParts.Count > 0 )
        {
            if (IsGameOver(deadParts))
            {
                //game over
            }
            else
            {
                List<BodyPart> bodiesToDrop = new List<BodyPart>();
                //throw dead parts
                foreach(BodyPartType partType in deadParts)
                {
                    Debug.Log(body[partType].ownerName);
                    bodiesToDrop.Add(body[partType]);
                    body[partType] = null;
                }
                Debug.Log(bodiesToDrop);
                Debug.Log(bodiesToDrop.Count);
                if (onDropParts != null)
                {
                    onDropParts(bodiesToDrop);
                }
            }
        }
        
    }

    private bool IsGameOver(List<BodyPartType> deadParts)
    {
        return (deadParts.Contains(BodyPartType.HandLeft) && deadParts.Contains(BodyPartType.HandRight))
            || (deadParts.Contains(BodyPartType.LegLeft) && deadParts.Contains(BodyPartType.LegRight));
        
    }
}
