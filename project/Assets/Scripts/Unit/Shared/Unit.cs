using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public abstract class Unit : MonoBehaviour
{
    public bool Player => _player;
    public float RotationSpeed => GetRotationSpeed();
    public float MovementSpeed => GetMoveSpeed();
    public bool ExploringStatus => _unitGroundController.IsExploring;

    [SerializeField] private bool _player = false;
    [SerializeField] private GameObject _colliderTrailPrefab;
    [SerializeField] private TrailRenderer _trailRenderer;

    [SerializeField] private Transform _heroViewParent;
    [SerializeField] protected UnitGroundController _unitGroundController;


    protected HeroView HeroView { get; private set; }
    protected HeroSkill _heroSkill;
    protected Unit _unit => this;

    private MovementService _movementService;
    private TrailService _trailService;
    private ColliderTrailService _colliderTrailService;
    private Owner _owner = new();


    public void Init(int size, CharacterSettings characterSettings)
    {
        _heroSkill = characterSettings.Skills;
        HeroView = Instantiate(characterSettings.Hero.Custom.HeroView, _heroViewParent);
        PrepareGroundServices(size, characterSettings);
        _movementService = new(unit: _unit);
        _colliderTrailService = new(colliderPrefab: _colliderTrailPrefab, this);
        InitForChildClass(characterSettings);
    }


    private void PrepareGroundServices(int size, CharacterSettings characterSettings)
    {
        _unitGroundController.OnPlayerInDeadhZone += Suicide;
        _trailService = new TrailService(_trailRenderer, characterSettings);
        _owner.Tile = characterSettings.Hero.Custom.MyGroundTile;
        _unitGroundController.Init(size, _owner);
    }

    private void Update()
    {
        _movementService.Update();
        _trailService.Update(ExploringStatus);
        _colliderTrailService.Update(ExploringStatus);
    }


    public abstract void TouchWorldWall();

    public abstract void KillEnemy();

    public abstract void DeadByOtherPlayer();

    public abstract void DeadTogetherOtherPlayer();
    protected abstract void InitForChildClass(CharacterSettings characterSettings);
    protected abstract void Destroy();


    protected abstract void Suicide();

    private float GetMoveSpeed()
    {
        return _heroSkill.MoveSpeed;
    }

    private float GetRotationSpeed()
    {
        return _heroSkill.RotationSpeed;
    }


    private void OnDestroy()
    {
        _unitGroundController.OnPlayerInDeadhZone -= Suicide;
        _colliderTrailService.Destroy();
        Destroy();
    }
}