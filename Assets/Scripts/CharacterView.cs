using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine.Networking;

public enum Direction
{
    UP,DOWN,RIGHT,LEFT,NONE
}
public enum AnimationState
{
    MOVING,IDLE,DIG
}

public class CharacterView: MonoBehaviour
{
    //
    public Direction direction = Direction.LEFT;
    //
    public AnimationState state = AnimationState.MOVING;
    public AnimationClip[] animationClips;
    private Dictionary<Direction, Animation> animations = new Dictionary<Direction, Animation>();
    private Animation digAnimation;
    private Dictionary<string, AnimationClip> clips = new Dictionary<string, AnimationClip>();
    private Animation current;
    
    private Dictionary<BodyPartType, string> animationPathMap = new Dictionary<BodyPartType, string>
    {
        { BodyPartType.Head, "but/torso/head" }, 
        { BodyPartType.HandLeft, "but/torso/LArm" }, 
        { BodyPartType.HandRight, "but/torso/RArm" }, 
        { BodyPartType.LegLeft, "L Pants" }, 
        { BodyPartType.LegRight, "R Pants" }, 
        { BodyPartType.Body, "but" }, 
    };

    // Use this for initialization
    void Awake()
    {
        animations[Direction.DOWN] = transform.Find("Front").GetComponent<Animation>();
        animations[Direction.UP] = transform.Find("Back").GetComponent<Animation>();
        animations[Direction.LEFT] = transform.Find("Left").GetComponent<Animation>();
        animations[Direction.RIGHT] = transform.Find("Right").GetComponent<Animation>();
        digAnimation = transform.Find("Dig").GetComponent<Animation>();


        foreach (var ac in animationClips)
        {
            clips[ac.name] = ac;
        }

        SetDirection(direction);
        SetState(state);
    }

    public void SetBodyPart(BodyPart part)
    {
        Replace("Front", part.type, part.front);
        Replace("Back", part.type, part.back);
        Replace("Left", part.type, part.left);
        Replace("Right", part.type, part.right);
        Replace("Dig", part.type, part.back);
    }
    
    private static Vector3 GetDirectionFromPart(BodyPartType part)
    {
        switch (part)
        {
            case BodyPartType.HandLeft:
            case BodyPartType.LegLeft:
                return Vector3.right;
            case BodyPartType.HandRight:
            case BodyPartType.LegRight:
                return Vector3.left;
        }
        return UnityEngine.Random.value > 0.5f ? Vector3.left : Vector3.right;
    }
    
    private static void Rot(Transform target)
    {
        var fxList = new List<SpriteRenderer>();
        fxList.Add(target.GetComponent<SpriteRenderer>());
        fxList.AddRange(target.GetComponentsInChildren<SpriteRenderer>());
        foreach (var item in fxList.Where(single => single != null))
        {
            var fx = item.gameObject.AddComponent<_2dxFX_DesintegrationFX>();
            fx.Seed = UnityEngine.Random.value;
            fx.Desintegration = 0;
            fx._Color = Color.green;
            DOVirtual.Float(0, 1, 2, number => fx.Desintegration = number);    
        }
    }

    public void RemovePart(BodyPart part)
    {
        var path = animationPathMap[part.type];
        
        // Toss a coin
        var frontItem = FindDeep("Front/" + path);
        var frontThing = Instantiate(part.front);
        frontThing.transform.SetParent(null);
        frontThing.transform.position = frontItem.transform.position;
        var randomRotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 35)));
        var direction = randomRotation * GetDirectionFromPart(part.type);
        var duration = 1.5f;
        var sequence = DOTween.Sequence();
        // sequence.Insert(0.1f, body[type].transform.DOLocalRotate(Vector3.forward * 450, duration - 0.2f).SetRelative(true));
        sequence.Insert(0,
            frontThing.transform.DOJump(transform.position + direction.normalized * 2, 0.75f, 2, duration));
        sequence.InsertCallback(duration + 0.5f, () => Rot(frontThing.transform));
        
        foreach (var animationType in new[]
        {
            "Front",
            "Back",
            "Left",
            "Right",
            "Dig",
        })
        {
            FindDeep(animationType + "/" + path).gameObject.SetActive(false);
        }
    }

    public void Replace(string animationType, BodyPartType bodyPartType, GameObject prefab)
    {
        var partObject = Instantiate(prefab, transform, false);
        var frontNewTransform = partObject.transform;
        var path = animationPathMap[bodyPartType];
        var frontOldTransform = FindDeep(animationType + "/" + path);
        frontNewTransform.SetParent(frontOldTransform.parent);
        frontNewTransform.position = frontOldTransform.position;
        frontNewTransform.rotation = frontOldTransform.rotation;
        frontNewTransform.localScale = frontOldTransform.localScale;
        
        if (bodyPartType == BodyPartType.Body)
        {
            var torso = frontNewTransform.Find("torso");
            // Get arms and head
            var head = FindDeep(animationType + "/" + animationPathMap[BodyPartType.Head]);
            var lHand = FindDeep(animationType + "/" + animationPathMap[BodyPartType.HandLeft]);
            var rHand = FindDeep(animationType + "/" + animationPathMap[BodyPartType.HandRight]);
            head.SetParent(torso, true);
            lHand.SetParent(torso, true);
            rHand.SetParent(torso, true);
        }
        frontNewTransform.gameObject.name = frontOldTransform.name;
        DestroyImmediate(frontOldTransform.gameObject);
    }
    
    public Transform FindDeep(string path)
    {
        var pathParts = path.Split('/');
        var currentTransform = transform;
        foreach (var pathPart in pathParts)
        {
            currentTransform = currentTransform.Find(pathPart);
        }
        return currentTransform;
    }


    public void SetState(AnimationState stateInput)
    {
        state = stateInput;
        string directionString = direction == Direction.LEFT || direction == Direction.RIGHT ?
            "Side_" : "Front_";
        string stateString = stateInput == AnimationState.IDLE ? "Idle" : "Move";
        string key = directionString + stateString;
        
        current.clip = clips[key];
        
        current.Play();
    }



    public void SetDirection(Direction directionInput)
    {
        foreach (KeyValuePair<Direction,Animation> item in animations)
        {
            if (directionInput != item.Key) item.Value.gameObject.SetActive(false);
            else
            {
                current = item.Value;
                current.gameObject.SetActive(true);
            }
        }
        direction = directionInput;
        digAnimation.gameObject.SetActive(false);
        SetState(state);
    }

    internal void StartDigging()
    {
        foreach (KeyValuePair<Direction, Animation> item in animations)
        {
            item.Value.gameObject.SetActive(false);
        }
        digAnimation.gameObject.SetActive(true);
        digAnimation.clip = clips["Dig_Enter"];
        digAnimation.Play();
    }

    internal void Dig(bool isLeft)
    {
        string animationClipName = isLeft ? "Dig_Right" : "Dig_Left";
        digAnimation.clip = clips[animationClipName];
        digAnimation.Play();
    }
}
