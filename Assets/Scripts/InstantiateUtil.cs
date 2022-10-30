using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUtil : MonoBehaviour
{
    [SerializeField] private GameObject _objectToInstantiate;

    public void InstantiateObject()
    {
        Instantiate(_objectToInstantiate);
    }
}
