using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class InFrontOf : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public int aboveSortingLayer = 1;
        public int belowSortingLayer = -1;
        private void Awake()
        {
            if (sprite == null)
                sprite = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            var controller = FindObjectOfType<PlayerController>();
            if (controller == null)
            {
                return;
            }
            var isInFrontOf = controller.transform.position.y > transform.position.y;
            sprite.sortingOrder = controller.sprite.sortingOrder + (isInFrontOf ? aboveSortingLayer : belowSortingLayer);
        }
        
    }
}