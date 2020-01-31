using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject holePrefab;

    public PlayerDigDetector digDetector;
    public IPlayerState playerState;
    public BodyPartsController bodyPartsController;
    public SpriteRenderer sprite;

    [Header("States")]
    public PlayerStateDigging stateDigging;
    public PlayerStateWalking stateWalking;

    public IPlayerState state;

    void Start()
    {
        state = stateWalking;
    }

    void Update()
    {
        state.Tick();
    }

    public void ChangeState(IPlayerState newState)
    {
        state.LeaveState(newState);
        newState.EnterState(state);
        state = newState;
    }
}
