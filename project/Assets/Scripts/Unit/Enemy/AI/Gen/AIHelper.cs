using System;
using System.Collections.Generic;
using UnityEngine;

public static class AIHelper
{
    public static Vector3 NearPointToTargetPoint(List<Vector3> points, Vector3 targetPoint)
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

    private static RaycastHit2D[] CastToAngle(Vector2 origin, float angleDegrees, float maxDistance)
    {
        Vector2 direction = Quaternion.Euler(0, 0, angleDegrees) * Vector2.right * maxDistance;
        var hits = Physics2D.RaycastAll(origin, direction, maxDistance);
        Debug.DrawRay(origin, direction, Color.green);
        return hits;
    }


    public static List<RaycastHit2D> CastAround(int countRaycast, Vector2 startCastPosition, float castDistance, Unit unit)
    {
        List<RaycastHit2D> targets = new();


        float angleStep = 360f / countRaycast;
        for (int i = 0; i < countRaycast; i++)
        {
            var currentAngle = angleStep * i;
            var hits = CastToAngle(startCastPosition, currentAngle, castDistance);

            foreach (var hit in hits)
            {
                if (hit.collider == null)
                {
                    continue;
                }

                if (hit.transform.gameObject == unit.gameObject)
                {
                    continue;
                }

                if (hit.transform.gameObject.TryGetComponent<TrailAgent>(out var trailAgent))
                {
                    if (trailAgent.Unit == unit)
                    {
                        continue;
                    }
                }

                targets.Add(hit);
            }
        }

        return targets;
    }


    public static Vector3 TryEnemyTarget(List<RaycastHit2D> casts, Vector3 myPosition)
    {
        Vector3 newTarget = Vector3.zero;

        float distance = Single.MaxValue;
        foreach (var cast in casts)
        {
            if (cast.transform.gameObject.TryGetComponent<TrailAgent>(out var trailAgent))
            {
                var distanceToCastPoint = Vector3.Distance(cast.point, myPosition);
                if (distanceToCastPoint < distance)
                {
                    distance = distanceToCastPoint;
                    newTarget = cast.point;
                }
            }
        }


        return newTarget;
    }
}