using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

/// <summary>
/// Ходит случайно но ближе к центру, видя колайдер игрока пытается напасть
/// </summary>
public class BrainGen4 : EnemyBrainBase
{
    private float _lastDistance = Single.MaxValue;


    private int _randomPointSelectCount = 5; // из скольки точек будет брать ближайшую к центру игры
    private float _sensorDistance = 20f;
    private int _countRaycasts = 12;

    private float _timeCheckEnemy = 0.5f;
    private float _currentTimer = 0f;

    protected override Vector3 GetTarget()
    {
        return _curentTarget;
    }

    protected override void Tick()
    {
        var curDistance = Vector3.Distance(_curentTarget, _transform.position);

        if (_currentTimer < _timeCheckEnemy)
        {
            _currentTimer += Time.deltaTime;
        }
        else
        {
            _currentTimer = 0f;
            // тут проверяка времени и делать через секунду проверку колайдера варага
            if (GeNearEnemyTarget(out var target))
            {
                _curentTarget = target;
                return;
            }
        }


        if (curDistance >= _lastDistance)
        {
            _curentTarget = GetRandomPointNearToCenter();
            _lastDistance = Single.MaxValue;
            return;
        }

        _lastDistance = curDistance;
    }

    private bool GeNearEnemyTarget(out Vector3 target)
    {
        var casts = CastMultipleRaycasts(12);
        Vector3 newTarget = Vector3.zero;

        float distance = Single.MaxValue;
        foreach (var cast in casts)
        {
            if (cast.transform.gameObject.TryGetComponent<TrailAgent>(out var trailAgent))
            {
                if (trailAgent.Unit == _unit)
                {
                    continue;
                }

                var distanceToCastPoint = Vector3.Distance(cast.point, _transform.position);
                if (distanceToCastPoint < distance)
                {
                    distance = distanceToCastPoint;
                    newTarget = cast.point;
                }
            }
        }

        if (newTarget != Vector3.zero)
        {
            target = newTarget;
            return true;
        }

        target = Vector3.zero;
        return false;
    }

    private Vector3 GetRandomPointNearToCenter()
    {
        List<Vector3> randomPoints = new();
        for (int i = 0; i < 3; i++)
        {
            randomPoints.Add(GetRandomBorderPosition());
        }

        var centerWorld = _worldController.CenterWorld;

        return NearPointToTargetPoint(randomPoints, centerWorld);
    }


    private Vector3 NearPointToTargetPoint(List<Vector3> points, Vector3 targetPoint)
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


    private List<RaycastHit2D> CastMultipleRaycasts(int countRaycast)
    {
        List<RaycastHit2D> hits = new();
        Vector2 origin = _transform.position;

        float angleStep = 360f / countRaycast;
        for (int i = 0; i < countRaycast; i++)
        {
            var currentAngle = angleStep * i;
            var hit = RaycastToAngle(origin, currentAngle, _sensorDistance);
            if (hit.collider != null)
            {
                hits.Add(hit);
            }
        }

        return hits;
    }

    private RaycastHit2D RaycastToAngle(Vector2 origin, float angleDegrees, float maxDistance)
    {
        Vector2 direction = Quaternion.Euler(0, 0, angleDegrees) * Vector2.right;
        //RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, obstacleLayer);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);
        return hit;
    }
}