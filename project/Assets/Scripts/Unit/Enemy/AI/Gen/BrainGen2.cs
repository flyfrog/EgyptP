using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

/// <summary>
/// Ходит случайно но с выбирает точки которые ближе к центру и выбирает ближайшие из Х
/// </summary>
public class BrainGen2 : EnemyBrainBase
{
    private float _lastDistance = Single.MaxValue;
  

    private int _randomPointSelectCount = 5; // из скольки точек будет брать ближайшую к центру игры

    protected override Vector3 GetTarget()
    {
        return _curentTarget;
    }

    protected override void Tick()
    {
        var curDistance = Vector3.Distance(_curentTarget, _transform.position);

        if (curDistance >= _lastDistance)
        {
            _curentTarget = GetNewTargetPosition();
            _lastDistance = Single.MaxValue;
            return;
        }

        _lastDistance = curDistance;
    }

    private Vector3 GetNewTargetPosition()
    {
        List<Vector3> randomPoints = new();
        for (int i = 0; i < 3; i++)
        {
            randomPoints.Add(GetRandomBorderPosition());
        }

        var centerWorld = _worldController.CenterWorld;

        return GetNearPointToTargetPoint(randomPoints, centerWorld);

    }

    private Vector3 GetNearPointToTargetPoint(List<Vector3> points, Vector3 targetPoint)
    {
        Vector3 nearPoint = new();
        float smallestDistance = Single.MaxValue;
        foreach (var point in points)
        {
            var distance = Vector3.Distance(point, targetPoint);
            if (distance< smallestDistance)
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