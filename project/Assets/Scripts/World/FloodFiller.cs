using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloodFiller
{
    int _groundPlayer = 1;
    int _groundFree = 0;
    int _groundFill = 2;
    private int[,] _grid;


    private Limits CalcLimits(List<Vector3Int> land)
    {
        Limits limits = new Limits();

        foreach (var point in land)
        {
            var x = point.x;
            var y = point.y;

            if (x > limits.maxX)
            {
                limits.maxX = x;
            }

            if (x < limits.minX)
            {
                limits.minX = x;
            }

            if (y > limits.maxY)
            {
                limits.maxY = y;
            }

            if (y < limits.minY)
            {
                limits.minY = y;
            }
        }

        limits.height = limits.maxY - limits.minY;
        limits.width = limits.maxX - limits.minX;


        return limits;
    }


    private int[,] SetGroundInCropGrid(int[,] grid, List<Vector3Int> land, Limits limits)
    {
        foreach (var point in land)
        {
            int x = point.x - limits.minX + 1;
            int y = point.y - limits.minY + 1;
            grid[x, y] = _groundPlayer;
        }

        return grid;
    }


    private void FloodFill(int pos_x, int pos_y, Limits limits)
    {
        if (_grid[pos_x, pos_y] == _groundPlayer || _grid[pos_x, pos_y] == _groundFill)
        {
            return;
        }


        _grid[pos_x, pos_y] = _groundFill;

        if (_grid.GetLength(0) > (pos_x + 1))
        {
            FloodFill(pos_x + 1, pos_y, limits); // then i can either go south
        }

        if ((pos_x - 1) >= 0)
        {
            FloodFill(pos_x - 1, pos_y, limits); // or north
        }


        if (_grid.GetLength(1) > (pos_y + 1))
        {
            FloodFill(pos_x, pos_y + 1, limits); // or east
        }

        if ((pos_y - 1) >= 0)
        {
            FloodFill(pos_x, pos_y - 1, limits); // or west
        }
    }

    private List<Vector3Int> ConvertCropToNormalList(int[,] grid, Limits limits)
    {
        var newGround = new List<Vector3Int>();

        for (int y = 0; y <= limits.height; y++)
        {
            for (int x = 0; x <= limits.width; x++)
            {
                if (grid[x, y] != _groundFill   )
                {
                    var point = new Vector3Int();
                    point.x = x + limits.minX - 1;
                    point.y = y + limits.minY - 1;
                    point.z = 0;
                    newGround.Add(point);
                }
            }
        }

        return newGround;
    }


    public List<Vector3Int> Fill(List<Vector3Int> land)
    {
        var limits = CalcLimits(land);
        int[,] cropGrid = new int[limits.width + 3, limits.height + 3];
        cropGrid = SetGroundInCropGrid(cropGrid, land, limits);
        _grid = cropGrid;
        FloodFill(0, 0, limits);

        var newGround = ConvertCropToNormalList(_grid, limits);
        return newGround;

    }

    public List<Vector3Int> GetOnlyNewGroundInAddedtGrounds(List<Vector3Int> newland, List<Vector3Int> oldLand)
    {
        return  newland.Except(oldLand).ToList();
    }


}