using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Loot : BaseLoot
{
    [SerializeField] private FlySettings _flySettings;
    [SerializeField] private LocatorType _locatorType;
    [SerializeField] private Transform _FX;
    public event Action OnCalledFinishAction;
    private bool _flyPermit;

    private LocatorManager _locatorManager;
    protected ILocatorAgent _myAgent;
    private FlyService _flyService;

    private GameManager _gameManager;

    [Inject]
    private void Construct(
        LocatorManager locatorManager,
        GameManager gameManager
    )
    {
        _locatorManager = locatorManager;
        _gameManager = gameManager;
        _gameManager.EndGame += DesroyItSelf;
        _myAgent = _locatorManager.GetLocatorAgent(_locatorType);
        _flyService = new(transform, _myAgent.Target, _flySettings, Finish);
    }

    private void OnDestroy()
    {
        _gameManager.EndGame -= DesroyItSelf;
    }

    public override void StartLooting()
    {
        base.StartLooting();
        ActivateWhenLooting();
        _flyPermit = true;
    }

    public override void LootingForEnemy()
    {
        DesroyItSelf();
    }

    private void Update()
    {
        if (!_flyPermit)
        {
            return;
        }


        _flyService.FlyTick();
    }

    private void ActivateWhenLooting()
    {
        _FX.gameObject.SetActive(true);
    }


    protected void DetachFX()
    {
        _FX.transform.parent = null;

        ParticleSystem fx = _FX.GetComponent<ParticleSystem>();
        fx.loop = false;
    }

    protected virtual void Finish()
    {
        DetachFX();
        OnCalledFinishAction?.Invoke();
        _myAgent.LocatorAction();
        DesroyItSelf();
    }

    protected void DesroyItSelf()
    {
        Destroy(gameObject);
    }
}