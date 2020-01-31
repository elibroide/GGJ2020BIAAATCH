using UnityEngine;

namespace Player
{
    public class PlayerStateDigging : MonoBehaviour, IPlayerState
    {
        public PlayerController controller;
        public Grave touchingGrave;
        public float moveAmount = 5.0f;
        
        [ReadOnly] public int clicks = 0;
        [ReadOnly] public bool isLeft;
        
        private void Awake()
        {
            GetComponentInParent<PlayerController>();
        }
        
        public void EnterState(IPlayerState previousState)
        {
            // Initialize
            clicks = 0;
            isLeft = false;
            
            // Do animation of enter state
            controller.sprite.color = Color.red;

            // Check collision with an area of dig
            if (controller.digDetector.touchingDig)
            {
                touchingGrave = controller.digDetector.touchingDig.parent;
            }
        }

        public void LeaveState(IPlayerState newState)
        {
            // Leave animation of dig state
            controller.sprite.transform.localPosition = Vector3.zero;
        }

        public void Tick()
        {
            var direction = Vector3.zero;
            var isHit = false;
            if (clicks == 0 || isLeft)
            {
                isHit = Input.GetKeyDown(KeyCode.A);
                direction = Vector3.left;
            }
            if (clicks == 0 || !isLeft)
            {
                isHit = Input.GetKeyDown(KeyCode.D);
                direction = Vector3.right;
            }

            if (isHit)
            {
                clicks++;
                isLeft = !isLeft;
                if (touchingGrave != null)
                {
                    touchingGrave.TakeHit(1);
                    if (touchingGrave.isDead)
                    {
                        controller.ChangeState(controller.stateWalking);
                    } 
                }
                controller.sprite.transform.localPosition = direction * moveAmount;
            }
        }
    }
}