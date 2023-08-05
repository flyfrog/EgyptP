using System;
using UnityEngine;
using Zenject;

public class ColliderTrailService
{
    private GameObject _colliderPrefab;
    private float colliderSpacing = 0.6f;

    private Vector2 lastPosition;
    private EdgeCollider2D _edgeCollider;
    private GameObject _colliderObj;
    private Transform _transform;
    private Unit _unit;

    public ColliderTrailService(
        GameObject colliderPrefab,
        Unit unit
    )
    {
        _unit = unit;
        _colliderPrefab = colliderPrefab;
        _transform = unit.gameObject.transform;
    }

    public void Start()
    {
        lastPosition = _transform.position;
        _colliderObj = GameObject.Instantiate(_colliderPrefab, lastPosition, Quaternion.identity);
        _colliderObj.GetComponent<TrailAgent>().Init(_unit);
        _edgeCollider = _colliderObj.GetComponent<EdgeCollider2D>();
    }

    public void Destroy()
    {
        if (_colliderObj!=null)
        {
            GameObject.Destroy(_colliderObj);
        }
    }

    private void DestroyCollider()
    {
        GameObject.Destroy(_colliderObj);
        _edgeCollider = null;
        _colliderObj = null;
    }


    public void Update(bool exploring)
    {
        if (_edgeCollider != null && exploring == false)
        {
            DestroyCollider();
            return;
        }

        if (_edgeCollider == null && exploring)
        {
            Start();
        }


        if (_edgeCollider == null)
        {
            return;
        }

        Vector2 currentPosition = _transform.position;
        float distance = Vector2.Distance(lastPosition, currentPosition);


        if (distance >= colliderSpacing)
        {
            UpdateCollider(currentPosition);
            lastPosition = currentPosition;
        }
    }

    private void UpdateCollider(Vector2 currentPosition)
    {
        Vector2[] colliderPoints = _edgeCollider.points;
        Vector2 newPoint = currentPosition - (Vector2)_colliderObj.transform.position;
        System.Array.Resize(ref colliderPoints, colliderPoints.Length + 1);
        colliderPoints[colliderPoints.Length - 1] = newPoint;

        _edgeCollider.points = colliderPoints;
    }
}