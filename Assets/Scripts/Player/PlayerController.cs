using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState;
    public BodyPartsController bodyPartsController;
    public SpriteRenderer sprite;

    [Header("States")]
    public PlayerState stateDigging;
    public PlayerState stateWalking;
    
    [ReadOnly]
    public PlayerState state;

    void Start()
    {
        state = stateWalking;
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
