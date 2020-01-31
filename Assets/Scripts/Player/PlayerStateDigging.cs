using UnityEngine;

namespace Player
{
    public class PlayerStateDigging : PlayerState
    {
        public PlayerController controller;
        public PlayerDigDetector detector;
        public Grave touchingGrave;
        public float moveAmount = 5.0f;
        
        [ReadOnly] public int clicks = 0;
        [ReadOnly] public bool isLeft;
        
        private void Awake()
        {
            GetComponentInParent<PlayerController>();
        }
        
        public override void EnterState(PlayerState previousState)
        {
            // Initialize
            clicks = 0;
            isLeft = false;
            
            // Do animation of enter state
            controller.sprite.color = Color.red;

            // Check collision with an area of dig
            if (detector.touchingDig)
            {
                touchingGrave = detector.touchingDig.parent;
            }
        }

        public override void LeaveState(PlayerState newState)
        {
            // Leave animation of dig state
            controller.sprite.transform.localPosition = Vector3.zero;
        }

        public override void Tick()
        {
            controller.bodyPartsController.Tick();
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