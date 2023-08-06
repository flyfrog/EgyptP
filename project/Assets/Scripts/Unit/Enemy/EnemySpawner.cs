using MyBox;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _playerPrefab;
    [SerializeField] private EnemyBrainBase[] _brains;

    private SpawnPointsController _spawnPointsController;
    private EnemySettingsController _enemySettingsController;

    private bool isSpawnedEnable = false;

    [Inject]
    private void Construct(
        SpawnPointsController spawnPointsController,
        EnemySettingsController enemySettingsController
    )
    {
        _spawnPointsController = spawnPointsController;
        _enemySettingsController = enemySettingsController;
    }

    public void FirstSpawn()
    {
        isSpawnedEnable = true;
        foreach (var enemy in _playerPrefab)
        {
            Spawn(enemy);
        }
    }


    private void Spawn(Enemy enemy)
    {
        var newEnemy = Instantiate(enemy, SelectSpawnPoint().position, Quaternion.identity, transform);

        var settings = _enemySettingsController.GetCharacterSettings();
        newEnemy.Init(10,settings);
        newEnemy.SetBrain(_brains[4]); // тут сатвлю поколение мозга которое хочу вставить в врага
    }

    public void StopSpawning()
    {
        isSpawnedEnable = false;
    }

    private Transform SelectSpawnPoint()
    {
        return _spawnPointsController.GetFreeSpawnPoint(10);
    }


    public void NeedSpawn()
    {
        if (!isSpawnedEnable)
        {
            return;
        }

        Spawn(_playerPrefab.GetRandom());
    }
}