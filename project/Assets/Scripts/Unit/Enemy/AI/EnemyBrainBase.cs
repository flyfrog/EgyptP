using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Zenject;

public abstract class EnemyBrainBase : MonoBehaviour
{


    protected Unit _unit;
    protected Transform _transform;
    protected UnitGroundController _unitGroundController;
    protected Vector3 _curentTarget;

    private bool _initComplite;
    protected TileTools _tileTools;
    protected WorldController _worldController;


    public void Init(
        Unit unit,
        UnitGroundController unitGroundController,
        TileTools tileTools,
        WorldController worldController
    )
    {
        _initComplite = true;
        _unit = unit;
        _transform = unit.gameObject.transform;
        _unitGroundController = unitGroundController;

        _tileTools = tileTools;
        _worldController = worldController;
    }


    protected abstract void Tick();
    protected abstract Vector3 GetTarget();



    private void Update()
    {
        Tick();
        _curentTarget = GetTarget();


        if (_curentTarget == Vector3.zero)
        {
            return;
        }

        Vector3 targetPos = _curentTarget;
        targetPos.z = _transform.position.z;

        Vector3 direction = targetPos - _transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float rotationStep = _unit.RotationSpeed * Time.deltaTime;

        float newAngle = Mathf.MoveTowardsAngle(_transform.eulerAngles.z, targetAngle, rotationStep);
        _transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }



}