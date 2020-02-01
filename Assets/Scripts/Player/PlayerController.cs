using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public bool isNotMove = false;
    public float totalGameTime = 5;
    public string characterName = "eli";
    public string characterOriginalBody = "zombit";
    public PlayerDigDetector detector;
    [FormerlySerializedAs("bodyPartsController")] public BodyController bodyController;
    public CharacterView view;
    public Rigidbody2D rigidbody;
    
    [Header("States")] public AreaOfDig digging; 

    [Header("States")]
    public PlayerState stateDigging;
    public PlayerState stateWalking;
    
    [ReadOnly]
    public PlayerState state;

    private float timeStarted;

    public event Action<bool> gameOver;

    void Awake()
    {
        bodyController = GetComponent<BodyController>();
        detector = GetComponent<PlayerDigDetector>();
    }

    void Start()
    {
        timeStarted = Time.time;
        bodyController.AllDropped += BodyControllerOnAllDropped;
        bodyController.AddedPart += BodyControllerOnAddedPart;
        bodyController.DropPart += BodyControllerOnDropPart;
        var factory = FindObjectOfType<BodyPartFactory>();
        var group = factory.GetBodyPartOfGroup(characterOriginalBody);
        foreach (var item in group)
        {
            bodyController.AddPart(item, characterName);
        }
        ChangeState(stateWalking);
    }

    void Update()
    {
        if (isNotMove)
        {
            return;
        }
        if (Time.time - timeStarted > totalGameTime)
        {
            isNotMove = true;
            GetComponent<PlayerMovementController>().Stop();
            gameOver?.Invoke(true);
            return;
        }
        state.Tick();
    }

    public void ChangeState(PlayerState newState)
    {
        state?.LeaveState(newState);
        newState.EnterState(state);
        state = newState;
    }

    public void StartDigging(AreaOfDig whatToDig)
    {
        digging = whatToDig; 
        ChangeState(stateDigging);
    }

    public void DoneDigging()
    {
        digging = null; 
        ChangeState(stateWalking);
    }

    public void PickUp(BodyPartPickup detectorPickup)
    {
        bodyController.AddPart(detectorPickup.data.parent, detectorPickup.data.ownerName);
        detectorPickup.PickedUp();
    }
    
    private void BodyControllerOnDropPart(BodyPartData obj)
    {
        view.RemovePart(obj.parent);
    }

    private void BodyControllerOnAddedPart(BodyPartData obj)
    {
        view.SetBodyPart(obj.parent);
    }
    
    private void BodyControllerOnAllDropped()
    {
        this.isNotMove = true;
        gameOver?.Invoke(false);
    }
}
