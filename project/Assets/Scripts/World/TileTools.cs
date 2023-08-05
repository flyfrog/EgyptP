using UnityEngine;
using Zenject;

public class TileTools
{
    private TileController _tileController;

    [Inject]
    public TileTools(
        TileController tileController
    )
    {
        _tileController = tileController;
    }

    public  Vector3Int GetGridPositionForWorldPoint(Vector3 position)
    {
        return _tileController.Map.WorldToCell(position);
    }

    public Vector3 GetWorldPositionForCell(Vector3Int position)
    {
        return _tileController.Map.GetCellCenterWorld(position);
    }
}