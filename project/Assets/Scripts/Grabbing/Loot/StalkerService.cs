using UnityEngine;

public class StalkerService : MonoBehaviour
{
    private Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        
        transform.position = _target.position;
    }
}