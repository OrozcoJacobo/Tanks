using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
//The turret script is actually responsible for creating and shooting the bullet in the direction that the turret is facing 
public class Turret : MonoBehaviour
{
    [SerializeField] private List<Transform> _turretBarrels;

    [SerializeField] private float _currentDelay = 0;

    [SerializeField] private bool _canShoot = true;

    [SerializeField] private int _bulletPoolCount = 10;

    public TurretData turretData;

    private Collider2D[] _tankColliders;

    private ObjectPool _bulletPool;

    [SerializeField] private UnityEvent _OnShoot, _OnReload, _OnCantShoot;
    [SerializeField] private UnityEvent<float> _OnReloading; 

    private void Awake()
    {
        _tankColliders = GetComponentsInParent<Collider2D>();
        _bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _bulletPool.Initialize(turretData.bulletPrefab, _bulletPoolCount);
        _OnReloading?.Invoke(_currentDelay);
    }

    private void Update()
    {
        if(_canShoot == false)
        {
            _currentDelay -= Time.deltaTime;
            _OnReloading?.Invoke(_currentDelay);
            if (_currentDelay <= 0)
            {
                _canShoot = true;
            }
        }
    }
    public void Shoot()
    {
        if(_canShoot)
        {
            _canShoot = false;
            _currentDelay = turretData.reloadDelay;

            foreach(var barrel in _turretBarrels)
            {
                //GameObject bullet = Instantiate(_bulletPrefab);
                GameObject bullet = _bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);
                foreach(var collider in _tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }

            _OnShoot?.Invoke();
            _OnReloading?.Invoke(_currentDelay);
        }
        else
        {
            _OnCantShoot?.Invoke();
        }
    }
}
