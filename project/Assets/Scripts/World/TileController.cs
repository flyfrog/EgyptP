using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class TileController : MonoBehaviour
{
    public Tilemap Map => _tilemap;
    [SerializeField] private Tilemap _tilemap;

    private WorldController _worldController;


    public void UpdateGround(List<Vector3Int> tilesForRedraw, Owner owner)
    {
        if (_tilemap == null)
        {
            //фикс бага при закрывтии игры
            return;
        }

        TileBase tile = null;
        if (owner != null)
        {
            tile = owner.Tile;
        }

        FillTiles(tilesForRedraw,tile );
    }

    public void Clear()
    {
        _tilemap.ClearAllTiles();
    }


    // потом не рисовать край карты так он нужен был только для отладки
    private void DrawOneTile(Vector3Int pos, TileBase tile)
    {
        _tilemap.SetTile(pos, tile);
    }

    private void FillTiles(List<Vector3Int> pos, TileBase tile)
    {
        Vector3Int[] posar = pos.ToArray();
        TileBase[] tiles = new TileBase [posar.Length];

        for (int i = 0; i < posar.Length; i++)
        {
            tiles[i] = tile;
        }

        _tilemap.SetTiles(posar, tiles);
    }
}