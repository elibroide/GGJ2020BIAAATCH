using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    [FormerlySerializedAs("bodyPartsController")] public BodyController bodyController;
    public SpriteRenderer sprite;
    public Rigidbody2D rigidbody;

    [Header("States")]
    public PlayerState stateDigging;
    public PlayerState stateWalking;
    
    [ReadOnly]
    public PlayerState state;

    void Start()
    {
        state = stateWalking;

        var factory = FindObjectOfType<BodyPartFactory>();
        var group = factory.GetBodyPartOfGroup("normal");
        foreach (var item in group)
        {
            bodyController.AddPart(item, "Eli");
        }
    }

    void Update()
    {
        state.Tick();
    }

    public void ChangeState(PlayerState newState)
    {
        state.LeaveState(newState);
        newState.EnterState(state);
        state = newState;
    }
}
