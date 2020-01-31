using System;
using UnityEngine;

namespace Player
{
    public class PlayerDigDetector : MonoBehaviour
    {
        public AreaOfDig touchingDig;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Touch");
            touchingDig = other.GetComponent<AreaOfDig>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("PlayerDigDetector - Leave");
            touchingDig = null;
        }
    }
}