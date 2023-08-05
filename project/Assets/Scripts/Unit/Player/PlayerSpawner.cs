using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] private Player _playerPrefab;

    private SpawnPointsController _spawnPointsController;
    private PlayerSettingsController _playerSettingsController;


    [Inject]
    private void Construct(

        SpawnPointsController spawnPointsController,
        PlayerSettingsController playerSettingsController
    )
    {
        _spawnPointsController = spawnPointsController;
        _playerSettingsController = playerSettingsController;
    }

    public void Spawn()
    {
        var player = Instantiate(_playerPrefab.gameObject, SelectSpawnPoint().position, Quaternion.identity, transform);
        var settings = _playerSettingsController.GetCharacterSettings();
        player.GetComponent<Player>().Init(10, settings);
    }

    private Transform SelectSpawnPoint()
    {
        return _spawnPointsController.GetFreeSpawnPoint(10);
    }
}