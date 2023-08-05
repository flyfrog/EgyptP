using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitGroundController : MonoBehaviour
{
    public event Action OnPlayerInDeadhZone;

    public bool IsExploring;
    private List<Vector3Int> _pathTilesBuffer = new();
    private List<Vector3Int> _myLand = new();
    private List<Vector3Int> _myLandOld = new();
    private FloodFiller _floodFiller = new();
    private WorldController _worldController;
    private TileTools _tileTools;

    private Owner _owner;
    private TileUtils _tileUtils = new();


    [Inject]
    private void  Construct(
        WorldController worldController,
        TileTools tileTools
    )
    {
        _worldController = worldController;
        _tileTools = tileTools;

    }


    public void Init(int startGroundSize, Owner owner)
    {
        _owner = owner;
        MakeMyStartGround(startGroundSize);
        _worldController.OnGroundChanged += GroundChangedNeedCheckGroundForAction;
    }


    private void OnDestroy()
    {
        Debug.Log("CharacterGroundController Destroy");
        _worldController.OnGroundChanged -= GroundChangedNeedCheckGroundForAction;
        _worldController.ClearGround(_myLand);
    }

    public List<Vector3Int> GetBorder()
    {
        return _tileUtils.FindBorder(_myLand, _worldController.World, _owner);
    }

    private void GroundChangedNeedCheckGroundForAction(List<Vector3Int> changedGround, Owner owner)
    {
        if (owner == null || owner == _owner)
        {
            return;
        }

        _myLand = _tileUtils.RemoveGroundFromGround(_myLand, changedGround);
        CheckForDeathZoneAndClearIfIsIt(changedGround, owner);
    }


    private void CheckForDeathZoneAndClearIfIsIt(List<Vector3Int> changedGround, Owner owner)
    {
        Vector3Int myCellPos = _tileTools.GetGridPositionForWorldPoint(transform.position);

        if (!changedGround.Contains(myCellPos))
        {
            return;
        }

        OnPlayerInDeadhZone?.Invoke();
    }


    private void MakeMyStartGround(int startGroundSize)
    {
        Vector3Int myCellPos = _tileTools.GetGridPositionForWorldPoint(transform.position);

        int startX = myCellPos.x - startGroundSize;
        int startY = myCellPos.y - startGroundSize;

        for (int x = startX; x < startX + (startGroundSize * 2); x++)
        {
            for (int y = startY; y < startY + (startGroundSize * 2); y++)
            {
                var tilePosition = new Vector3Int(x, y, 0);

                _myLand.Add(tilePosition);
            }
        }

        SetMyGround();
    }


    private void Update()
    {
        Vector3Int myPositionCellCoordinate = _tileTools.GetGridPositionForWorldPoint(transform.position);
        myPositionCellCoordinate.z = 0;

        if (_pathTilesBuffer.Contains(myPositionCellCoordinate))
        {
            return;
        }

        if (!_worldController.CheckGroundOwner(_owner, myPositionCellCoordinate))
        {
            IsExploring = true;
            AddInPathNewGroundOneTile(myPositionCellCoordinate);
            return;
        }

        if (_worldController.CheckGroundOwner(_owner, myPositionCellCoordinate) &&
            _pathTilesBuffer.Count > 0
           )
        {
            IsExploring = false;
            EndPath();
            return;
        }
    }

    private void AddInPathNewGroundOneTile(Vector3Int shortCellPosition)
    {
        _pathTilesBuffer.Add(shortCellPosition);
    }

    private void EndPath()
    {
        GrabNewArea(_pathTilesBuffer);
        _pathTilesBuffer.Clear();
    }

    private void GrabNewArea(List<Vector3Int> path)
    {
        AddNewGroundInMyLand(path);
        var newGroundFilled = _floodFiller.Fill(_myLand);
        AddNewGroundInMyLand(newGroundFilled);

        SetMyGround();
    }

    private void SetMyGround()
    {
        var grabbedLand = _floodFiller.GetOnlyNewGroundInAddedtGrounds(_myLand, _myLandOld);

        _worldController.SetOwnerForGround(grabbedLand, _owner);
        _myLandOld = new();
        _myLandOld.AddRange(_myLand);
    }


    private void AddNewGroundInMyLand(List<Vector3Int> newLands)
    {
        foreach (var tile in newLands)
        {
            if (!_myLand.Contains(tile))
            {
                _myLand.Add(tile);
            }
        }
    }



}