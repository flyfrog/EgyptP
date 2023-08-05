using System;
using UnityEngine;

public abstract class BaseLoot : MonoBehaviour, ILootable
{
    protected Vector3Int _myGridCoordinate;
    public event Action OnLootingStarted;


    public void SetCellPosition(Vector3Int pos)
    {
        _myGridCoordinate = pos;
    }


    public virtual void StartLooting()
    {
        OnLootingStarted?.Invoke();
    }

    public abstract void LootingForEnemy();
}