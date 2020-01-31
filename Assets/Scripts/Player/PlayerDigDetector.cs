using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerDigDetector : MonoBehaviour
    {
        public List<AreaOfDig> touchingDig;
        public List<BodyPartPickup> pickup;

        public AreaOfDig GetDig()
        {
            return touchingDig.FirstOrDefault();
        }

        public BodyPartPickup GetPickup()
        {
            return pickup.FirstOrDefault();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Touch");
            var touchingDig = other.GetComponent<AreaOfDig>();
            if (touchingDig != null)
            {
                this.touchingDig.Add(touchingDig);
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
            }
            else
            {
                this.pickup.Remove(other.GetComponent<BodyPartPickup>());
            }
        }
    }
}