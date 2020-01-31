using UnityEngine;

namespace Player
{
    public class PlayerStateWalking : IPlayerState
    {
        private PlayerController _controller;
        
        public PlayerStateWalking(PlayerController controller)
        {
            _controller = controller;
        }
        
        public void EnterState(IPlayerState previousState)
        {
            
        }

        public void LeaveState(IPlayerState newState)
        {
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Perform attack
                _controller.ChangeState(new PlayerStateDigging(_controller));
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
            _controller.movementController.SetDirection(direction);
        }
    }
}