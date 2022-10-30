using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    [SerializeField]private AIBehaviour shootBehaviour, patrolBehavoiur;

    [SerializeField] private TankController _tank;
    [SerializeField] private AIDetector _detector;

    private void Update()
    {
        if(_detector.TargetVisible)
        {
            shootBehaviour.PerformAction(_tank, _detector);
        }
        else
        {
            patrolBehavoiur.PerformAction(_tank, _detector);
        }
    }
}
