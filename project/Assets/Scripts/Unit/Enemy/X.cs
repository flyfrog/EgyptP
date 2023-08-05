using System.Collections.Generic;
using UnityEngine;

public class X : MonoBehaviour
{
    [SerializeField] private LayerMask _nodeMask;
    [SerializeField] private float _radious = 1f;

    [MyBox.ButtonMethod()]
    public IEnumerable<Collider2D> ReturnNear()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radious);

        Debug.Log(colliders.Length);
        return colliders;
    }
}