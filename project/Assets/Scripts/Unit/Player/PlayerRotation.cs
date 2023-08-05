using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Camera mainCamera;

    private Unit _unit;
    protected Transform _transform;
    private bool _initComplite;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Init(
        Unit unit
        )
    {
        _initComplite = true;
        _unit = unit;
        _transform = unit.gameObject.transform;
    }

    private void Update()
    {
        if (!_initComplite)
        {
            return;
        }

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = _transform.position.z;

        Vector3 direction = mousePosition - _transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float rotationStep = _unit.RotationSpeed * Time.deltaTime;

        float newAngle = Mathf.MoveTowardsAngle(_transform.eulerAngles.z, targetAngle, rotationStep);
        _transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }
}