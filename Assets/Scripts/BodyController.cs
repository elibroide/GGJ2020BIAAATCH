using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
    

public class BodyController : MonoBehaviour
{
    public Dictionary<BodyPartType, BodyPartController> body = new Dictionary<BodyPartType, BodyPartController>();
    Action<List<BodyPartData>> OnDropParts;

    // Start is called before the first frame update
    public void AddPart(BodyPart part, string ownerName)
    {
        if (body.ContainsKey(part.type))
        {
            DropPartFromBody(part.type);
        }
        // TODO: Check if part exists and drop it
        var bodyPartInstance = Instantiate(part.bodyPartController, transform, false);
        bodyPartInstance.data = part.CreateData(ownerName);
        body.Add(part.type, bodyPartInstance.GetComponent<BodyPartController>());
    }

    public void Tick()
    {
        // decompose all
        float delta = Time.deltaTime;
        
        var deadParts = new List<BodyPartData>();

        foreach (var part in body.Values)
        {
            part.data.Decompose(delta);
            if (part.data.health <= 0) deadParts.Add(part.data);
        }

        if (deadParts.Count > 0 )
        {
            if (IsGameOver(deadParts))
            {
                //game over
            }
            else
            {
                var bodiesToDrop = new List<BodyPartData>();
                //throw dead parts
                foreach(var part in deadParts)
                {
                    DropPartFromBody(part.type);
                    Debug.Log(part.ownerName);
                    bodiesToDrop.Add(part);
                }
                Debug.Log(bodiesToDrop);
                Debug.Log(bodiesToDrop.Count);
                
                OnDropParts?.Invoke(bodiesToDrop);
            }
        }
        
    }

    private bool IsGameOver(List<BodyPartData> deadParts)
    {
        return body.Count == 0;
    }

    private void DropPartFromBody(BodyPartType type)
    {
        if (body.ContainsKey(type))
        {
            body[type].transform.SetParent(null);
            body.Remove(type);
        }
    }
}
