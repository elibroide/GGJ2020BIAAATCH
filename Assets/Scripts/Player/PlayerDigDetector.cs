using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerDigDetector : MonoBehaviour
    {
        public Action<AreaOfDig> enteredAreaOfDig;
        public Action<AreaOfDig> leftAreaOfDig;
        
        public List<AreaOfDig> touchingDig;
        public List<BodyPartPickup> pickup;

        public AreaOfDig GetDig()
        {
            return touchingDig.FirstOrDefault();
        }

        public BodyPartPickup GetPickup()
        {
            SoundManager.PlaySound("pick up body part");
            return pickup.FirstOrDefault();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Touch");
            var touchingDig = other.GetComponent<AreaOfDig>();
            if (touchingDig != null)
            {
                this.touchingDig.Add(touchingDig);
                enteredAreaOfDig?.Invoke(touchingDig);
                if (!touchingDig.parent.isOpened)
                {
                    touchingDig.parent.Peek();
                }
            }
            else
            {
                this.pickup.Add(other.GetComponent<BodyPartPickup>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Leave");
            var touchingDig = other.GetComponent<AreaOfDig>();
            if (touchingDig != null)
            {
                this.touchingDig.Remove(touchingDig);
                leftAreaOfDig?.Invoke(touchingDig);
                touchingDig.parent.HidePeek();
            }
            else
            {
                this.pickup.Remove(other.GetComponent<BodyPartPickup>());
            }
        }
    }
}