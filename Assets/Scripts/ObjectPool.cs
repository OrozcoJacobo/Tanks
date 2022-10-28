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

    public void Initialize(GameObject objectToPool, int poolSize = 10)
    {
        _objectToPool = objectToPool;
        _poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObject = null;

        if (_objectPool.Count < _poolSize)
        {
            spawnedObject = Instantiate(_objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + _objectToPool.name + "_" + _objectPool.Count;
            spawnedObject.transform.SetParent(_spawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisabled>();
        }
        else
        {
            spawnedObject = _objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        _objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded()
    {
        if(_spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + _objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
                _spawnedObjectsParent = parentObject.transform;
            else
            {
                _spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }
    private void OnDestroy()
    {
        foreach(var item in _objectPool)
        {
            if (item == null)
                continue;
            else if (item.activeSelf == false)
                Destroy(item);
            else
                item.GetComponent<DestroyIfDisabled>().selfDestructionEnabled = false;
        }
    }
}
