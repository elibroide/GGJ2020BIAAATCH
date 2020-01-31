using UnityEngine;

namespace Player
{
    public class PlayerStateDigging : IPlayerState
    {
        public int clicks = 0;
        public bool isLeft;
        
        private PlayerController _controller;
        
        public PlayerStateDigging(PlayerController controller)
        {
            _controller = controller;
        }
        
        public void EnterState(IPlayerState previousState)
        {
            // Do animation of enter state
        }

        public void LeaveState(IPlayerState newState)
        {
            // Leave animation of dig state
        }

        public void Tick()
        {
            if (clicks == 0 || isLeft)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isLeft = !isLeft;
                }    
            }
            if (clicks == 0 || !isLeft)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isLeft = !isLeft;
                }    
            }
        }
    }
}