using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject holePrefab; 
        
    public IPlayerState playerState;
    public BodyPartsController bodyPartsController;
    public PlayerMovementController movementController;
    public SpriteRenderer sprite;

    public IPlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new PlayerStateWalking(this));
    }

    // Update is called once per frame
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

    private void CheckAttack()
    {
        DOTween.Sequence();
    }
}
