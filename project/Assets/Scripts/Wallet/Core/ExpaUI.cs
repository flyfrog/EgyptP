using MyBox;
using TMPro;
using UnityEngine;
using Zenject;

public class ExpaUI : LocatorAgent
{
    [SerializeField] private Animation _addExpaAnimation;
    [SerializeField] private GameObject _Fxprefab;
    [SerializeField] private Transform _FxParent;
    [SerializeField] private Transform _FxSpawnPoint;



    public override void LocatorAction()
    {
        _addExpaAnimation.Play();
        Instantiate(_Fxprefab,_FxSpawnPoint.position , Quaternion.identity, _FxParent);
    }



}