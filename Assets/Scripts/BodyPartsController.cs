using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class BodyPartsController : MonoBehaviour
{
    Dictionary<BodyPartType, BodyPartController> body = new Dictionary<BodyPartType, BodyPartController>();
    Action<List<BodyPartController>> onDropParts;

    // Start is called before the first frame update
    void Start()
    {
        body[BodyPartType.Head] = new BodyPartController(BodyPartType.Head,1, "head");
        body[BodyPartType.HandRight]= new BodyPartController(BodyPartType.HandRight, 2,"hr");
        body[BodyPartType.HandLeft] = new BodyPartController(BodyPartType.HandLeft, 3, "hl");
        body[BodyPartType.LegLeft] = new BodyPartController(BodyPartType.LegLeft, 2, "ll");
        body[BodyPartType.LegRight] = new BodyPartController(BodyPartType.LegRight, 3, "lr");
    }

    public void Tick()
    {
        
        // decompose all
        float delta = Time.deltaTime;
        
        List<BodyPartType> deadParts = new List<BodyPartType>();

        foreach (KeyValuePair<BodyPartType, BodyPartController>  entry in body)
        {
            BodyPartController part = entry.Value;
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
                List<BodyPartController> bodiesToDrop = new List<BodyPartController>();
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
