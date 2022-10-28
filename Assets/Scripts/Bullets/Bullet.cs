using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _maxDistance = 50;

    private Vector2 _startPosition;
    private float _conquaredDistance = 0;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _conquaredDistance = Vector2.Distance(transform.position, _startPosition);
        if(_conquaredDistance > _maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);    
    }

    public void Initialize()
    {
        _startPosition = transform.position;
        _rb.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider" + other.name);

        var damagable = other.GetComponent<Damagable>();
        if(damagable != null)
        {
            damagable.Hit(_damage);
        }

        DisableObject();
    }
}
