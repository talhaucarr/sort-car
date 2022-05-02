using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarMovement
{
    void GetStartPoint();
    void CheckTargetReached();
    void ChangeMovementType(MovementType type);
    void GetEmptyPoint();
    void Move(Vector3 dir);
    void Turn(Quaternion rotation);
}
