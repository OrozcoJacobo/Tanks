using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolStaticBehaviour : AIBehaviour
{
    public float patrolDelay = 4;

    [SerializeField] private Vector2 _randomDirection = Vector2.zero;
    [SerializeField] private float _currentPatrolDelay;

    private void Awake()
    {
        _randomDirection = Random.insideUnitCircle;
    }
    public override void PerformAction(TankController tank, AIDetector detector)
    {
        float angle = Vector2.Angle(tank.aimTurret.transform.right, _randomDirection);
        if(_currentPatrolDelay <= 0 && (angle < 2))
        {
            _randomDirection = Random.insideUnitCircle;
            _currentPatrolDelay = patrolDelay;
        }
        else
        {
            if (_currentPatrolDelay > 0)
                _currentPatrolDelay -= Time.deltaTime;
            else
                tank.HandleTurretMovement((Vector2)tank.aimTurret.transform.position + _randomDirection);
        }
    }
}
