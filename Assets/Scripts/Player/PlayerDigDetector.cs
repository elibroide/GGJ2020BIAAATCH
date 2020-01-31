using System;
using UnityEngine;

namespace Player
{
    public class PlayerDigDetector : MonoBehaviour
    {
        public AreaOfDig touchingDig;
        public BodyPartPickup pickup;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Touch");
            var touchingDig = other.GetComponent<AreaOfDig>();
            if (touchingDig != null)
            {
                this.touchingDig = touchingDig;
            }
            else
            {
                this.pickup = other.GetComponent<BodyPartPickup>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Leave");
            var touchingDig = other.GetComponent<AreaOfDig>();
            if (touchingDig != null)
            {
                this.touchingDig = null;
            }
            else
            {
                this.pickup = null;
            }
        }
    }
}