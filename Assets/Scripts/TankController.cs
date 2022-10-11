using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private Rigidbody2D _rb;


    private Vector2 _movementVector;


    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _turretRotationSpeed = 150;
    [SerializeField] private Transform _turretParent;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    private void FixedUpdate()
    {
        _rb.velocity = (Vector2)transform.up * _movementVector.y * _maxSpeed * Time.deltaTime;
        _rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -_movementVector.x * _rotationSpeed * Time.deltaTime));
    }

    public void HandleShoot()
    {

    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        this._movementVector = movementVector;
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - _turretParent.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = _turretRotationSpeed * Time.deltaTime;
        _turretParent.rotation = Quaternion.RotateTowards(_turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationStep);
    }
}
