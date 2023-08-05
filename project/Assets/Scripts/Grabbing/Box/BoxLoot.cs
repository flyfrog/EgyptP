
using UnityEngine;

public abstract class BoxLoot : BaseLoot
{
    public BoxType BoxType => _boxType;
    [SerializeField] internal BoxType _boxType;
    public BaseLoot[] Loots => _loots;
    [SerializeField] internal BaseLoot[] _loots;

    public BoxState BoxState => _currentBoxState;
    internal BoxState _currentBoxState = BoxState.Lock;

    public void SetCellPosition(Vector3Int pos)
    {
        _myGridCoordinate = pos;
    }

    public abstract void OpenBox();


    public abstract void AnimationEventEndOpen();

    public abstract void AnimationEventEndGrab();


}