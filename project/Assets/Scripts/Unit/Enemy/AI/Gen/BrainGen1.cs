using System;
using MyBox;
using UnityEngine;

/// <summary>
/// Просто ходит случайно от края до края
/// </summary>
public class BrainGen1 : EnemyBrainBase
{
    private float _lastDistance = Single.MaxValue;
 
    protected override Vector3 GetTarget()
    {
        return _curentTarget;
    }

    protected override void Tick()
    {
        var curDistance = Vector3.Distance(_curentTarget, _transform.position);

        if (curDistance>=_lastDistance)
        {

            _curentTarget = _tileTools.GetWorldPositionForCell(GetRandom());
            _lastDistance = Single.MaxValue;
            return;
        }

        _lastDistance = curDistance;

    }

    private  Vector3Int GetRandom()
    {
        var border = _unitGroundController.GetBorder();
        var randomPoint = border.GetRandom();

        return randomPoint;
    }

}