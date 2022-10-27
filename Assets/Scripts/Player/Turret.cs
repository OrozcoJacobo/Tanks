using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
//The turret script is actually responsible for creating and shooting the bullet in the direction that the turret is facing 
public class Turret : MonoBehaviour
{
    [SerializeField] private List<Transform> _turretBarrels;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _reloadDelay = 1;
    [SerializeField] private float _currentDelay = 0;

    [SerializeField] private bool _canShoot = true;

    private Collider2D[] _tankColliders;

    private ObjectPool _bulletPool;
    [SerializeField] private int _bulletPoolCount = 10;

    private void Awake()
    {
        _tankColliders = GetComponentsInParent<Collider2D>();
        _bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _bulletPool.Initialize(_bulletPrefab, _bulletPoolCount);
    }

    private void Update()
    {
        if(_canShoot == false)
        {
            _currentDelay -= Time.deltaTime;
            if(_currentDelay <= 0)
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
            _currentDelay = _reloadDelay;

            foreach(var barrel in _turretBarrels)
            {
                //GameObject bullet = Instantiate(_bulletPrefab);
                GameObject bullet = _bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize();
                foreach(var collider in _tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }
}
