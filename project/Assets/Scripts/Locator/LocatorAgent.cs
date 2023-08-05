using UnityEngine;
using Zenject;

public class LocatorAgent : MonoBehaviour, ILocatorAgent
{
    public LocatorType LocatorType => _locatorType;
    public Transform Target => target;

    [SerializeField] private LocatorType _locatorType;
    [SerializeField] private Transform target;

    private LocatorManager _locatorManager;

    [Inject]
    private void Construct(
        LocatorManager locatorManager
    )
    {
        _locatorManager = locatorManager;
        _locatorManager.Register(this);
    }

    public virtual void LocatorAction()
    {
        throw new System.NotImplementedException();
    }


}