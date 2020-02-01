using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
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

    void Awake()
    {
        bodyController = GetComponent<BodyController>();
        detector = GetComponent<PlayerDigDetector>();
    }

    void Start()
    {
        bodyController.AddedPart += BodyControllerOnAddedPart;
        bodyController.DropPart += BodyControllerOnDropPart;
        var factory = FindObjectOfType<BodyPartFactory>();
        var other = factory.GetBodyPartOfGroup("other");
        var group = factory.GetBodyPartOfGroup(characterOriginalBody);
        foreach (var item in group)
        {
            if (item.type == BodyPartType.Head)
            {
                var otherHead = other.FirstOrDefault(ii => ii.type == BodyPartType.Head);
                bodyController.AddPart(otherHead, characterName);
            }
            else
            {
                bodyController.AddPart(item, characterName);
            }
        }
        ChangeState(stateWalking);
    }

    void Update()
    {
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
        view.RemovePart(obj.type);
    }

    private void BodyControllerOnAddedPart(BodyPartData obj)
    {
        view.SetBodyPart(obj.parent);
    }
}
