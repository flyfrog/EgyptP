﻿using System;
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
    private float _sensorDistance = 10f;
    private int _countRaycasts = 12;

    private float _timeCheckEnemy = 0.6f;
    private float _currentTimer = 0f;

    protected override Vector3 GetTarget()
    {
        return _curentTarget;
    }

    protected override void Tick()
    {
        if (_currentTimer < _timeCheckEnemy)
        {
            _currentTimer += Time.deltaTime;
        }
        else
        {
            _currentTimer = 0f;
            var casts = AIHelper.CastAround(
                countRaycast: 12,
                startCastPosition: _transform.position,
                castDistance: _sensorDistance,
                unit: _unit
            );

            var enemyTarget = AIHelper.TryEnemyTarget(casts, _transform.position);
            if (enemyTarget != Vector3.zero)
            {
                _curentTarget = enemyTarget;
                _lastDistance = Single.MaxValue;
                return;
            }
        }

        var curDistance = Vector3.Distance(_curentTarget, _transform.position);
        if (curDistance >= _lastDistance)
        {
            _curentTarget = GetRandomPointNearToCenter();
            _lastDistance = Single.MaxValue;
            return;
        }

        _lastDistance = curDistance;
    }


    private Vector3 GetRandomPointNearToCenter()
    {
        List<Vector3> randomPoints = new();
        for (int i = 0; i < 3; i++)
        {
            randomPoints.Add(GetRandomBorderPosition());
        }

        var centerWorld = _worldController.CenterWorld;
        return AIHelper.NearPointToTargetPoint(randomPoints, centerWorld);
    }

    private Vector3 GetRandomBorderPosition()
    {
        var border = _unitGroundController.GetBorder();
        var randomPoint = border.GetRandom();
        return _tileTools.GetWorldPositionForCell(randomPoint);
    }
}