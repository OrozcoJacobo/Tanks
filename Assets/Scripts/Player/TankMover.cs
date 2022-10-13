using System;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _acceleration = 70.0f;
    [SerializeField] private float _deacceleration = 0;
    [SerializeField] private float _currentSpeed = 0f;
    [SerializeField] private float _currentForwardDirection = 1f;

    private Vector2 _movementVector;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = (Vector2)transform.up * _currentSpeed * _currentForwardDirection * Time.deltaTime;
        _rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -_movementVector.x * _rotationSpeed * Time.deltaTime));
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
            _currentSpeed += _acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _deacceleration * Time.deltaTime;
        }
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }
}
