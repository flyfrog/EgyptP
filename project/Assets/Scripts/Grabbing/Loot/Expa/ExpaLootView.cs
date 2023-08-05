using UnityEngine;
using Zenject;

public class ExpaLootView : MonoBehaviour
{
    [SerializeField] private Loot _loot;

    private HeroController _heroController;
    private int _expaValue;

    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
    }

    public void SetExpaValue(int expa)
    {
        _expaValue = expa;
    }

    private void Awake()
    {
        _loot.OnCalledFinishAction += FinishAction;
    }

    private void OnDestroy()
    {
        _loot.OnCalledFinishAction -= FinishAction;
    }

    private void FinishAction()
    {
        _heroController.AddExpaForCurrentHero(_expaValue);
    }
}
