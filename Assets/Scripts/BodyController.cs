using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Player;
using UnityEngine;
using Random = System.Random;


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
            body[type].Rot();
            body[type].transform.SetParent(null);
            Quaternion randomRotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));

            var sequence = DOTween.Sequence();
            sequence.Insert(0.5f, body[type].transform.DOJump(transform.position +  randomRotation * Vector3.up * 2f, 0.5f, 1, 0.5f));
            body.Remove(type);
        }
    }
}
