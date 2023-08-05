
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileUtils
{
    public List<Vector3Int> FindBorder(List<Vector3Int> land, Dictionary<Vector3Int, TileData> _tileWorld, Owner owner)
    {
        List<Vector3Int> bordersCells = new();

        foreach (var cell in land)
        {
            var left = new Vector3Int(cell.x - 1, cell.y, 0);
            var right = new Vector3Int(cell.x + 1, cell.y, 0);
            var top = new Vector3Int(cell.x, cell.y + 1, 0);
            var down = new Vector3Int(cell.x, cell.y - 1, 0);

            var downRight = new Vector3Int(cell.x + 1, cell.y - 1, 0);
            var downLeft = new Vector3Int(cell.x - 1, cell.y - 1, 0);

            var topRight = new Vector3Int(cell.x + 1, cell.y + 1, 0);
            var topLeft = new Vector3Int(cell.x - 1, cell.y + 1, 0);

            if (!_tileWorld.ContainsKey(left) || _tileWorld[left].Owner != owner)
            {
                bordersCells.Add(cell);
                continue;
            }


            if (!_tileWorld.ContainsKey(right) || _tileWorld[right].Owner != owner)
            {
                bordersCells.Add(cell);
                continue;
            }

            if (!_tileWorld.ContainsKey(top) || _tileWorld[top].Owner != owner)
            {
                bordersCells.Add(cell);
                continue;
            }

            if (!_tileWorld.ContainsKey(down) || _tileWorld[down].Owner != owner)
            {
                bordersCells.Add(cell);
                continue;
            }


            if (cell.y % 2 != 0)
            {
                if (_tileWorld.ContainsKey(topRight) && _tileWorld[topRight].Owner != owner)
                {
                    bordersCells.Add(cell);
                    continue;
                }

                if (_tileWorld.ContainsKey(downRight) && _tileWorld[downRight].Owner != owner)
                {
                    bordersCells.Add(cell);
                    continue;
                }
            }


            //  четное
            if (cell.y % 2 == 0)
            {
                if (_tileWorld.ContainsKey(topLeft) && _tileWorld[topLeft].Owner != owner)
                {
                    bordersCells.Add(cell);
                    continue;
                }

                if (_tileWorld.ContainsKey(downLeft) && _tileWorld[downLeft].Owner != owner)
                {
                    bordersCells.Add(cell);
                    continue;
                }
            }
        }

        return bordersCells;
    }

    public List<Vector3Int> RemoveGroundFromGround(List<Vector3Int> baseGround, List<Vector3Int> groundForRemove)
    {
        var containsGround = baseGround.Intersect(groundForRemove);
        return baseGround.Except(containsGround).ToList();
    }
}