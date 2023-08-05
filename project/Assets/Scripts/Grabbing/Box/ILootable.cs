using System;

public interface ILootable
{
    public event Action OnLootingStarted;

    public  void StartLooting();
    void LootingForEnemy();
}