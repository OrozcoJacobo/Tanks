using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject _objectToPool;
    [SerializeField] protected int _poolSize = 10;

    //Queue data structure, first in first in first out 
    protected Queue<GameObject> _objectPool;

    [SerializeField] private Transform _spawnedObjectsParent;

    private void Awake()
    {
        _objectPool = new Queue<GameObject>();
    }

    public void Initialize()
}
