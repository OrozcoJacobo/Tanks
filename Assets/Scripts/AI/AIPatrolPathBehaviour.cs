using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolPathBehaviour : AIBehaviour
{
    [SerializeField] private PatrolPath _patrolPath;
    [Range(0.1f, 1)]
    [SerializeField] private float _arriveDistance = 1;
    [SerializeField] private float _waitTime = 0.5f;
    [SerializeField] private bool _isWaiting = false;
    [SerializeField] Vector2 currentPatrolTarget = Vector2.zero;
    
    bool isInitialized = false;

    private int _currentIndex = -1;

    private void Awake()
    {
        if (_patrolPath == null)
            _patrolPath = GetComponentInChildren<PatrolPath>();
    }

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (!_isWaiting)
        {
            if (_patrolPath.Length < 2)
                return;
            if (!isInitialized)
            {
                var currentPathPoint = _patrolPath.GetClosestPathPoint(tank.transform.position);
                this._currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInitialized = true;
            }
            if (Vector2.Distance(tank.transform.position, currentPatrolTarget) < _arriveDistance)
            {
                _isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }
            Vector2 directionToGo = currentPatrolTarget - (Vector2)tank.tankMover.transform.position;
            var dotProduct = Vector2.Dot(tank.tankMover.transform.up, directionToGo.normalized);

            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(tank.tankMover.transform.up, directionToGo.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                tank.HandleMoveBody(new Vector2(rotationResult, 1));
            }
            else
            {
                tank.HandleMoveBody(Vector2.up);
            }

        }

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_waitTime);
            var nextPathPoint = _patrolPath.GetNextPathPoint(_currentIndex);
            currentPatrolTarget = nextPathPoint.Position;
            _currentIndex = nextPathPoint.Index;
            _isWaiting = false;
        }
    }
}
