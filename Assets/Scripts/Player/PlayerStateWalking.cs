using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerStateWalking : PlayerState
    {
        public PlayerController controller;
        public PlayerMovementController movementController;

        public override void EnterState(PlayerState previousState)
        {
            controller.sprite.color = Color.white;
        }

        public override void LeaveState(PlayerState newState)
        {
        }

        public override void Tick()
        {
            controller.bodyPartsController.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Perform attack
                controller.ChangeState(controller.stateDigging);
                return;
            }
            var direction = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
            movementController.SetDirection(direction);
        }
    }
}