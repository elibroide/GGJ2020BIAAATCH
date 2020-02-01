using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    //[ReadOnly]
    public Direction direction = Direction.LEFT;
    //[ReadOnly]
    public AnimationState state = AnimationState.MOVING;
    public AnimationClip[] animationClips;
    private Dictionary<Direction, Animation> animations = new Dictionary<Direction, Animation>();
    private Animation digAnimation;
    private Dictionary<string, AnimationClip> clips = new Dictionary<string, AnimationClip>();
    private Animation current;

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
            Debug.Log(ac.name);
            clips[ac.name] = ac;
        }

        SetDirection(direction);
        SetState(state);
    }


    public void SetState(AnimationState stateInput)
    {
        state = stateInput;
        string directionString = direction == Direction.LEFT || direction == Direction.RIGHT ?
            "Side_" : "Front_";
        string stateString = stateInput == AnimationState.IDLE ? "Idle" : "Move";
        string key = directionString + stateString;
        Debug.Log(key);
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
