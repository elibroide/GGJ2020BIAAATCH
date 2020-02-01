using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DG.Tweening;
using Player;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;


public class BodyController : MonoBehaviour
{
    public Dictionary<BodyPartType, BodyPartData> body = new Dictionary<BodyPartType, BodyPartData>
    {
        { BodyPartType.Head, null },
        { BodyPartType.HandLeft, null },
        { BodyPartType.HandRight, null },
        { BodyPartType.LegLeft, null },
        { BodyPartType.LegRight, null },
    };
    Action<List<BodyPartData>> OnDropParts;

    public event Action<BodyPartData> DropPart;
    public event Action<BodyPartData> AddedPart;
    private GUIController _guiController;

    void Awake()
    {
        _guiController = FindObjectOfType<GUIController>();
    }

    // Start is called before the first frame update
    public void AddPart(BodyPart part, string ownerName)
    {
        if (body[part.type] != null)
        {
            DropPartFromBody(part.type);
        }
        body[part.type] = part.CreateData(ownerName);
        AddedPart?.Invoke(body[part.type]);
    }

    public void Tick()
    {
        // decompose all
        float delta = Time.deltaTime;
        
        var deadParts = new List<BodyPartData>();

        foreach (var part in body.Values.Where(value => value != null))
        {
            part.Decompose(delta);
            if (part.health <= 0) deadParts.Add(part);
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
        
        _guiController.UpdateBodyState(body);        
    }

    private bool IsGameOver(List<BodyPartData> deadParts)
    {
        return body.Count == 0;
    }

    private void DropPartFromBody(BodyPartType type)
    {
        if (body[type] != null)
        {
            body[type] = null;
            DropPart?.Invoke(body[type]);
        }
    }
}
