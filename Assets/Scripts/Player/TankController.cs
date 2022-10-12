using UnityEngine;

public class TankController : MonoBehaviour
{


    public AimTurret aimTurret;
    public TankMover tankMover;

    private void Awake()
    {
        if(tankMover == null)
            tankMover = GetComponentInChildren<TankMover>();
        if (aimTurret == null)
            aimTurret = GetComponentInChildren<AimTurret>();
    }

    public void HandleShoot()
    {

    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        tankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        aimTurret.Aim(pointerPosition);

    }
}
