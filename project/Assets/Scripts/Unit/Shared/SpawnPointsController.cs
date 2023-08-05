using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnPointsController : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _freeAreaAroundSpawnArea = 4;

    private TileTools _tileTools;
    private WorldController _worldController;

    [Inject]
    private void Construct(
        TileTools tileTools,
        WorldController worldController
    )
    {
        _tileTools = tileTools;
        _worldController = worldController;
    }

    public Transform GetFreeSpawnPoint(int spawnSize)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            if (CheckAreaForFree(spawnSize, spawnPoint))
            {
                return spawnPoint;
            }
        }

        return null;
    }


    private bool CheckAreaForFree(int size, Transform point)
    {
        Vector3Int myCellPos = _tileTools.GetGridPositionForWorldPoint(point.position);
        List<Vector3Int> land = new();

        int sizeWithFreeZone = size + _freeAreaAroundSpawnArea;
        int startX = myCellPos.x - sizeWithFreeZone;
        int startY = myCellPos.y - sizeWithFreeZone;

        for (int x = startX; x < startX + (sizeWithFreeZone * 2); x++)
        {
            for (int y = startY; y < startY + (sizeWithFreeZone * 2); y++)
            {
                var tilePosition = new Vector3Int(x, y, 0);

                land.Add(tilePosition);
            }
        }


        foreach (var tile in land)
        {
            if (!_worldController.CheckGroundOwner(null,tile))
            {
                return false;
            }

        }

        return true;
    }
}