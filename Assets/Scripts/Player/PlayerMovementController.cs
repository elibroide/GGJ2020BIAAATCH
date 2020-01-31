using DG.Tweening;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    
    public float maxSpeed;
    public float accelerateTime;
    public float decelerateTime;

    [ReadOnly] public float targetSpeed;
    [ReadOnly] public float speed;
    [ReadOnly] public Vector2 direction;

    private float _currentTargetSpeed;
    private bool _currentIsAccelerating;
    private Tween _speedTween;
    
    /*
     *
     *    Interface
     * 
     */

    public void SetDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            targetSpeed = maxSpeed;
            this.direction = direction;
        }
        else
        {
            targetSpeed = 0;
        }
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        this.maxSpeed = maxSpeed;
    }

    public void Stop()
    {
        targetSpeed = 0;
    }
    
    /*
     *
     *    Unity
     * 
     */
    
    void Update()
    {
        CalcSpeed();
        Move();
    }

    private void CalcSpeed()
    {
        if (_currentTargetSpeed == targetSpeed) return;
        
        _speedTween?.Kill();
        var duration = targetSpeed == 0 ? accelerateTime : decelerateTime;
        _speedTween = DOTween.To(value => speed = value, speed, targetSpeed, duration)
            .SetTarget(this);
        _currentTargetSpeed = targetSpeed;
    }

    private void Move()
    {
        rigidbody.velocity = direction.normalized * (Time.deltaTime * speed);
    }
}
