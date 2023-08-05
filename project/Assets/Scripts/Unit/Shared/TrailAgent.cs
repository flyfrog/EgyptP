using UnityEngine;

public class TrailAgent : MonoBehaviour
{
    public Unit Unit =>_unit;
    private Unit _unit;

    public void Init(
        Unit unit
    )
    {
        _unit = unit;
    }



}