using System;
using UnityEngine;

public class FlyService
{
    private float _currentLerp;
    private Action _finishAction;
    private Vector3 _startFlyPosition;

    private bool _isFirstTick = true;

    private FlySettings _flySettings;
    private Transform _myTransform;
    private Transform _target;

    public FlyService(
        Transform myTransform,
        Transform target,
        FlySettings flySettings,
        Action finishAction)
    {
        _finishAction = finishAction;
        _flySettings = flySettings;
        _myTransform = myTransform;
        _target = target;

    }

    public void FlyTick()
    {
        if (_isFirstTick)
        {

            _isFirstTick = false;
            _startFlyPosition = _myTransform.position;
        }

        _currentLerp += _flySettings.FlySpeed * Time.deltaTime;
        _currentLerp = Mathf.Clamp01(_currentLerp);

        var finish = Camera.main.ScreenToWorldPoint( _target.position);

        var lerpForPosition = _flySettings.FlyDynamicCurve.Evaluate(_currentLerp);
        Vector3 pos = Vector3.Lerp(_startFlyPosition, finish, lerpForPosition);
        pos.x += _flySettings.XShiftCurve.Evaluate(_currentLerp) * _flySettings.ShiftXMultiplier;
        _myTransform.position = pos;

        if (_currentLerp >= 1)
        {
            _finishAction.Invoke();
        }
    }
}