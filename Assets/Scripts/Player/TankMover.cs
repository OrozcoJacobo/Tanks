using System;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    private Rigidbody2D _rb;

    public TankMovementData movementData;

    private float _currentSpeed = 0f;
    private float _currentForwardDirection = 1f;

    private Vector2 _movementVector;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = (Vector2)transform.up * _currentSpeed * _currentForwardDirection * Time.deltaTime;
        _rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -_movementVector.x * movementData.rotationSpeed * Time.deltaTime));
    }

    public void Move(Vector2 movementVector)
    {
        this._movementVector = movementVector;
        CalculateSpeed(movementVector);
        if (movementVector.y > 0)
            _currentForwardDirection = 1;
        else if(movementVector.y < 0)
            _currentForwardDirection = 0;
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if(Mathf.Abs(movementVector.y) > 0)
        {
            _currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, movementData.maxSpeed);
    }
}
