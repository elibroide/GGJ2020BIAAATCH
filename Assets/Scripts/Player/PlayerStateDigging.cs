using UnityEngine;

namespace Player
{
    public class PlayerStateDigging : PlayerState
    {
        public PlayerController controller;
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
            Camera.main.GetComponent<CameraController>().FocusIn();
            
            // Do animation of enter state
            controller.sprite.color = Color.red;

        }

        public override void LeaveState(PlayerState newState)
        {
            // Leave animation of dig state
            controller.sprite.transform.localPosition = Vector3.zero;
            Camera.main.GetComponent<CameraController>().FocusOut();
        }

        public override void Tick()
        {
            controller.bodyController.Tick();
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
                if (controller.digging.parent != null)
                {
                    controller.digging.parent.TakeHit(1);
                    if (controller.digging.parent.isDead)
                    {
                        controller.DoneDigging();
                    } 
                }
                controller.sprite.transform.localPosition = direction * moveAmount;
            }
        }
    }
}