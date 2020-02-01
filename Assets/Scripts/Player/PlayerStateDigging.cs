using UnityEngine;

namespace Player
{
    public class PlayerStateDigging : PlayerState
    {
        public PlayerController controller;
        public float moveAmount = 5.0f;
        
        [ReadOnly] public int clicks = 0;
        [ReadOnly] public bool isLeft;

        public float shakeOnDigDuration = 0.1f;
        public float shakeOnDigStrength = 0.3f;
        
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
            GUIController.Instance.ShowDiggingScreen();

            // Do animation of enter state
            controller.view.StartDigging();

            controller.transform.position = controller.digging.transform.position;
            controller.digging.parent.HidePeek();
        }

        public override void LeaveState(PlayerState newState)
        {
            // Leave animation of dig state
            controller.view.transform.localPosition = Vector3.zero;
            Camera.main.GetComponent<CameraController>().FocusOut();
            GUIController.Instance.HideDiggingScreen();
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.digging.parent.Peek();
                controller.DoneDigging();
                return;
            }
            controller.bodyController.Tick();
            var isLeftClicked = false;
            var direction = Vector3.zero;
            var isHit = false;
            if (clicks == 0 || isLeft)
            {
                isLeftClicked = true;
                isHit = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
                direction = Vector3.left;
            }
            if (clicks == 0 || !isLeft)
            {
                isLeftClicked = false;
                isHit = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
                direction = Vector3.right;
            }

            if (isHit)
            {
                clicks++;
                isLeft = !isLeft;
                if (controller.digging.parent != null)
                {
                    if (controller.bodyController.body
                            [isLeftClicked ? BodyPartType.HandLeft : BodyPartType.HandRight] != null)
                    {
                        FindObjectOfType<CameraController>().Shake(shakeOnDigDuration, shakeOnDigStrength);
                        controller.digging.parent.TakeHit(1);
                        
                        if (controller.digging.parent.isOpened)
                        {
                            controller.digging.parent.HidePeek();
                            controller.DoneDigging();
                        } 
                    }
                    
                }
                controller.view.Dig(isLeftClicked);
                controller.view.transform.localPosition = direction * moveAmount;
            }
        }
    }
}