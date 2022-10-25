using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The turret script is actually responsible for creating and shooting the bullet in the direction that the turret is facing 
public class Turret : MonoBehaviour
{
    [SerializeField] private List<Transform> _turretBarrels;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _reloadDelay = 1;
    [SerializeField] private float _currentDelay = 0;

    [SerializeField] private bool _canShoot = true;

    private Collider2D[] _tankColliders;


    private void Awake()
    {
        _tankColliders = GetComponentsInParent<Collider2D>();
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
                GameObject bullet = Instantiate(_bulletPrefab);
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
