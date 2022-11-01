using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject[] _enemiesLeft;
    private GameObject _playerAlive;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        _enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        _playerAlive = GameObject.FindGameObjectWithTag("Player");
        if (_enemiesLeft != null && _enemiesLeft.Length == 0)
        {
            SceneManager.LoadScene(0);
        }
        if (_playerAlive == null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
