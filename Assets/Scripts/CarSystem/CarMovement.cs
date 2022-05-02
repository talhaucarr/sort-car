using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour, ICarMovement
{
    [SerializeField] private float speed = 50;

    public bool IsMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    public Vector3 NextDir { get; private set; }

    public Route CurrentRoute { get; private set; }
    private Route _startingNode;
    private bool _isMoving;

    public void GetEmptyPoint()
    {
        float minAngle = Mathf.Infinity;

        if (CurrentRoute.NextRoutes.Count == 0)
        {
            ChangeMovementType(MovementType.Stop);
            return;
        }

        foreach (var rt in CurrentRoute.NextRoutes)
        {
            if (!rt.IsEmpty)
            {
                ChangeMovementType(MovementType.Stop);
                continue;
            }
            minAngle = GetAngleBetweenCarAndPoint(minAngle, rt);
        }
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir * Time.deltaTime * speed;
    }

    public void Turn(Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public void ChangeMovementType(MovementType type)
    {
        switch (type)
        {
            case MovementType.Stop:
                _isMoving = false;
                break;
            case MovementType.Moving:
                _isMoving = true;
                break;
        }
    }

    public void CheckTargetReached()
    {
        if (Vector3.Distance(CurrentRoute.transform.position, transform.position) < .5f)
        {
            CurrentRoute.IsEmpty = false;
            GetEmptyPoint();
            NextDir = (CurrentRoute.transform.position - transform.position).normalized;
        }
    }

    public void GetStartPoint()
    {

        GetClosestPoint();
        NextDir = (_startingNode.transform.position - transform.position).normalized;
        CurrentRoute = _startingNode;
    }

    private void GetClosestPoint()
    {
        _startingNode = RouteManager.Instance.GetClosestRoute(transform);
    }

    private float GetAngleBetweenCarAndPoint(float minAngle, Route rt)
    {
        var angle = Vector3.Angle(NextDir, (rt.transform.position - transform.position).normalized);
        if (angle < minAngle)
        {
            CurrentRoute.IsEmpty = true;
            minAngle = angle;
            CurrentRoute = rt;
            ChangeMovementType(MovementType.Moving);
        }

        return minAngle;
    }
}
