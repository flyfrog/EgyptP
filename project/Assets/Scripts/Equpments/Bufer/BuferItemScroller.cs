using System;
using UnityEngine;
using UnityEngine.UI;

public class BuferItemScroller : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private ScrollRect _scroll;
    [SerializeField] private float _speed = 0.1f;
    private bool _animationState;
    private float _curent;
    private Direction _direction = Direction.Left;

    private void OnEnable()
    {
        StartAnimation();
    }

    private void OnDestroy()
    {
        StopAnimation();
    }


    private void StartAnimation()
    {
        _animationState = true;
    }

    private void StopAnimation()
    {
        _animationState = false;
    }

    private void Update()
    {
        if (!_animationState)
        {
            return;
        }


        _curent += Time.deltaTime * _speed;
        _scroll.horizontalNormalizedPosition = _curve.Evaluate(Mathf.PingPong(_curent, 1));

    }


}