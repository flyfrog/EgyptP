using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Zenject;

public class Chest : BoxLoot
{
    [SerializeField] private AnimationStateReference _openAnimation;
    [SerializeField] private AnimationStateReference _grabAnimation;

    private WorldController _worldController;
    private GameManager _gameManager;

    [Inject]
    private void Construct(
        WorldController worldController,
        GameManager gameManager
    )
    {
        _worldController = worldController;
        _gameManager = gameManager;

        if (!_worldController.CheckGroundOwner(null, _myGridCoordinate))
        {
            OpenBox();
        }

        _gameManager.EndGame += DesroyItSelf;
        _worldController.OnGroundChanged += CheckMyGround;
    }

    private void OnDestroy()
    {
        _gameManager.EndGame -= DesroyItSelf;
        _worldController.OnGroundChanged -= CheckMyGround;
    }

    public override void OpenBox()
    {
        OpenBoxAnim();
    }



    public override void StartLooting()
    {
        base.StartLooting();

        if (_currentBoxState != BoxState.Open)
        {
            return;
        }

        _currentBoxState = BoxState.Empty;


        GrabBoxAnim();
    }

    public override void LootingForEnemy()
    {
        if (_currentBoxState != BoxState.Open)
        {
            return;
        }

        DesroyItSelf();
    }

    public override void AnimationEventEndOpen()
    {
        _currentBoxState = BoxState.Open;
    }

    public override void AnimationEventEndGrab()
    {
        StartCoroutine(SpawnLoot());
    }

    private void CheckMyGround(List<Vector3Int> changedLands, Owner owner)
    {
        if (changedLands.Contains(_myGridCoordinate) && owner != null)
        {
            OpenBox();
        }
    }


    private IEnumerator SpawnLoot()
    {
        foreach (var loot in _loots)
        {
            var newLoot = Instantiate(loot);
            newLoot.gameObject.transform.position = transform.position;
            newLoot.StartLooting();
            yield return new WaitForSeconds(0.1f);
        }


        DesroyItSelf();
    }


    private void OpenBoxAnim()
    {
        _openAnimation.Play();
    }


    private void GrabBoxAnim()
    {
        _grabAnimation.Play();
    }

    private void DesroyItSelf()
    {
        Destroy(gameObject);
    }
}