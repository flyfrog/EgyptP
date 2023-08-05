using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

/// <summary>
/// Делает задержку перед тем как повернуть и выбирает ближайшую точку границы своей земли из Х точек
/// </summary>
public class BrainGen3 : EnemyBrainBase
{
    private enum MoveState
    {
        GoToTarget,
        GoForward
    }

 

    private int _randomPointSelectCount = 8; // из скольки точек будет брать ближайшую к центру игры

    private float _delayBeforeChangeDirection = 0.4f;
    private float _curentTimer = 0f;
    private MoveState _moveState = MoveState.GoForward;
    private float _distanceForReliseTarget = 0.2f;

    protected override Vector3 GetTarget()
    {
        return _curentTarget;
    }

    protected override void Tick()
    {
        if (_moveState == MoveState.GoToTarget)
        {
            var curDistance = Vector3.Distance(_curentTarget, _transform.position);
            if (curDistance > _distanceForReliseTarget)
            {
                return;
            }

            if (curDistance <= _distanceForReliseTarget)
            {
                _moveState = MoveState.GoForward;
                _curentTarget = Vector3.zero;
                return;
            }
        }

        if (_moveState == MoveState.GoForward)
        {
            if (_curentTimer < _delayBeforeChangeDirection)
            {
                _curentTarget = Vector3.zero;
                _curentTimer += Time.deltaTime;
                return;
            }

            if (_curentTimer > _delayBeforeChangeDirection)
            {
                _curentTimer = 0f;
                _curentTarget = GetNewTargetPosition();
                _moveState = MoveState.GoToTarget;
                return;
            }
        }
    }

    private Vector3 GetNewTargetPosition()
    {
        List<Vector3> randomPoints = new();
        for (int i = 0; i < 3; i++)
        {
            randomPoints.Add(GetRandomBorderPosition());
        }

        return GetNearPointToTargetPoint(randomPoints, _transform.position);
    }

    private Vector3 GetNearPointToTargetPoint(List<Vector3> points, Vector3 targetPoint)
    {
        Vector3 nearPoint = new();
        float smallestDistance = Single.MaxValue;
        foreach (var point in points)
        {
            var distance = Vector3.Distance(point, targetPoint);
            if (distance < smallestDistance)
            {
                nearPoint = point;
                smallestDistance = distance;
            }
        }

        return nearPoint;
    }

    private Vector3 GetRandomBorderPosition()
    {
        var border = _unitGroundController.GetBorder();
        var randomPoint = border.GetRandom();
        return _tileTools.GetWorldPositionForCell(randomPoint);
    }
}