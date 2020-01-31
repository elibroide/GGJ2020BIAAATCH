using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovementController movementController;
    public SpriteRenderer sprite;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Perform attack
            isAttacking = true;
            movementController.Stop();
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0, () => sprite.color = Color.red);
            sequence.InsertCallback(2, () => { 
                isAttacking = false;
                sprite.color = Color.white;
            });
            return;
        }
        var direction = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        movementController.SetDirection(direction);
    }

    private void CheckAttack()
    {
        DOTween.Sequence();
    }
}
