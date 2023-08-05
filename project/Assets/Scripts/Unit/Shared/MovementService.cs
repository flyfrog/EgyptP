using UnityEngine;

public class MovementService
{
    private Transform _transform;

    private Unit _unit;

    public MovementService(
        Unit unit
    )
    {
        _transform = unit.gameObject.transform;
        _unit = unit;
    }


    public void Update()
    {
        _transform.Translate(Vector3.right * _unit.MovementSpeed * Time.deltaTime);
    }
}