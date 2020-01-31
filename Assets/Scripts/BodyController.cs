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
    public Dictionary<BodyPartType, BodyPartController> body = new Dictionary<BodyPartType, BodyPartController>
    {
        { BodyPartType.Head, null },
        { BodyPartType.HandLeft, null },
        { BodyPartType.HandRight, null },
        { BodyPartType.LegLeft, null },
        { BodyPartType.LegRight, null },
    };
    Action<List<BodyPartData>> OnDropParts;
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
        var bodyPartInstance = Instantiate(part.bodyPartController, transform, false);
        bodyPartInstance.data = part.CreateData(ownerName);
        body[part.type] = bodyPartInstance.GetComponent<BodyPartController>();
    }

    public void Tick()
    {
        // decompose all
        float delta = Time.deltaTime;
        
        var deadParts = new List<BodyPartData>();

        foreach (var part in body.Values.Where(value => value != null))
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
        
        _guiController.UpdateBodyState(body);        
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
            var randomRotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 35)));
            var direction = randomRotation * (UnityEngine.Random.value > 0.5f ? Vector3.left : Vector3.right);
            var duration = 1.5f;
            var sequence = DOTween.Sequence();
            sequence.Insert(0, body[type].transform.DOLocalRotate(Vector3.forward * 450, duration).SetRelative(true));
            sequence.Insert(0,
                body[type].transform.DOJump(transform.position + direction.normalized * 2, 0.75f, 2, duration));
            sequence.InsertCallback(0.6f, body[type].Rot);
            body[type] = null;
        }
    }
}
