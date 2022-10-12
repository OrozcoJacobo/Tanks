using UnityEngine;

public class TankMover : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 100f;

    private Vector2 _movementVector;
    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = (Vector2)transform.up * _movementVector.y * _maxSpeed * Time.deltaTime;
        _rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -_movementVector.x * _rotationSpeed * Time.deltaTime));
    }

    public void Move(Vector2 movementVector)
    {
        this._movementVector = movementVector;
    }
}
