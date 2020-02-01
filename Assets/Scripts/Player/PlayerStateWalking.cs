using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerStateWalking : PlayerState
    {
        public PlayerController controller;
        public PlayerMovementController movementController;
        
        public float fullSpeed;
        public float halfSpeed;
        public float lowSpeed = 0.1f;

        private Vector2 _currentDirection;

        public override void EnterState(PlayerState previousState)
        {
            controller.view.SetState(AnimationState.IDLE);
            
            controller.detector.enteredAreaOfDig += EnteredAreaOfDig;
            controller.detector.leftAreaOfDig += LeftAreaOfDig;
        }

        public override void LeaveState(PlayerState newState)
        {
            controller.detector.enteredAreaOfDig -= EnteredAreaOfDig;
            controller.detector.leftAreaOfDig -= LeftAreaOfDig;
            SoundManager.StopWalk();
        }

        public override void Tick()
        {
            controller.bodyController.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Perform attack
                CheckAction();
                return;
            }
            var direction = Vector2.zero;
            Direction directionState = Direction.NONE;
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
                directionState = Direction.LEFT;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
                directionState = Direction.UP;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
                directionState = Direction.DOWN;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
                directionState = Direction.RIGHT;
            }
            if (direction != _currentDirection)
            {
                movementController.SetDirection(direction);
                _currentDirection = direction;

                if (directionState == Direction.NONE)
                {
                    controller.view.SetState(AnimationState.IDLE);
                    SoundManager.StopWalk();
                }
                else
                {
                    Debug.Log(directionState);
                    SoundManager.Walk();
                    controller.view.SetDirection(directionState);
                    controller.view.SetState(AnimationState.MOVING);
                }
            }
            
            SetSpeed();
        }

        private void SetSpeed()
        {
            // Set speed
            var legCount = controller.bodyController.body[BodyPartType.LegLeft] != null ? 1 : 0;
            legCount += controller.bodyController.body[BodyPartType.LegRight] != null ? 1 : 0;
            var speed = fullSpeed;
            if (legCount == 0)
            {
                speed = lowSpeed;
            } else if (legCount == 1)
            {
                speed = halfSpeed;
            }
            movementController.SetMaxSpeed(speed);
        }

        private void CheckAction()
        {
            if (controller.detector.GetPickup() != null)
            {
                // Pick that sh#@@! up
                controller.PickUp(controller.detector.GetPickup());
                return;
            }

            if (controller.detector.GetDig() != null)
            {
                movementController.Stop();
                controller.StartDigging(controller.detector.GetDig());
                return;
            }
            
            // Create a hole and start digging it?
        }
        
        private void EnteredAreaOfDig(AreaOfDig obj)
        {
            
        }
        
        private void LeftAreaOfDig(AreaOfDig obj)
        {
        }
    }
}