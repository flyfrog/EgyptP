using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorldController
{
    public event Action<List<Vector3Int>, Owner> OnGroundChanged;
    public WorldLimits WorldLimits => _worldLimits;

    public Vector3 CenterWorld => _centerWorld;
    public Dictionary<Vector3Int, TileData> World => _world;
    private Dictionary<Vector3Int, TileData> _world;
    private TileTools _tileTools;
    private WorldLimits _worldLimits = new();
    private TileController _tileController;
    private const int WORLD_WIDTH = 250;

    private const int WORLD_HEIGHT = 250;
    private Vector3 _centerWorld;

    [Inject]
    public WorldController(
        TileController tileController,
        TileTools tileTools
    )
    {
        _tileController = tileController;
        _tileTools = tileTools;
    }


    public void MakeGameZone()
    {
        _world = GenerateGrid(WORLD_WIDTH, WORLD_HEIGHT);


        var leftLimits = _tileTools.GetWorldPositionForCell(new Vector3Int(0, 0, 0));
        var rightLimits = _tileTools.GetWorldPositionForCell(new Vector3Int(WORLD_WIDTH, WORLD_HEIGHT, 0));
        _worldLimits.minX = Math.Min(leftLimits.x, rightLimits.x);
        _worldLimits.maxX = Math.Max(leftLimits.x, rightLimits.x);

        _worldLimits.minY = Math.Min(leftLimits.y, rightLimits.y);
        _worldLimits.maxY = Math.Max(leftLimits.y, rightLimits.y);

        _centerWorld = _tileTools.GetWorldPositionForCell(new Vector3Int(WORLD_WIDTH/2, WORLD_HEIGHT/2, 0));
    }

    private Dictionary<Vector3Int, TileData> GenerateGrid(int wWorldSize, int hWorldSize)
    {
        Dictionary<Vector3Int, TileData> grid = new Dictionary<Vector3Int, TileData>();

        for (int x = 0; x < wWorldSize; x++)
        {
            for (int y = 0; y < hWorldSize; y++)
            {
                TileData tileData = new TileData();
                grid.Add(new Vector3Int(x, y, 0), tileData);

                //TileBase tile = new Tile();

                // if (
                //     x==0 ||
                //     y==0 ||
                //     x==wWorldSize-1 ||
                //     y== hWorldSize-1
                //     )
                // {
                //     tile = _borderTile;
                //   _tileController.DrawOneTile(new Vector3Int(x, y, 0), tile);
                // }
            }
        }

        return grid;
    }


    public void Clear()
    {
        _world.Clear();
        _world = GenerateGrid(WORLD_WIDTH, WORLD_HEIGHT);
        _tileController.Clear();
    }

    public void SetOwnerForGround(List<Vector3Int> newLands, Owner owner)
    {
        foreach (var tile in newLands)
        {
            _world[tile].Owner = owner;
        }

        UpdateGroundForSubscribers(newLands, owner);
    }


    public void ClearGround(List<Vector3Int> newLands)
    {
        foreach (var tile in newLands)
        {
            _world[tile].Owner = null;
        }

        UpdateGroundForSubscribers(newLands, null);
    }


    private void UpdateGroundForSubscribers(List<Vector3Int> newLands, Owner owner)
    {
        OnGroundChanged?.Invoke(newLands, owner);
        _tileController.UpdateGround(newLands, owner);
    }

    public bool CheckGroundOwner(Owner owner, Vector3Int position)
    {
        if (_world[position].Owner == owner)
        {
            return true;
        }

        return false;
    }
}