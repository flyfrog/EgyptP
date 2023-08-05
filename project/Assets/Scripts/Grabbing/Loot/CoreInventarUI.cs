using System;
using MyBox;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CoreInventarUI : LocatorAgent
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationStateReference _addLootAnimation;
    [SerializeField] private GameObject _Fxprefab;
    [SerializeField] private Transform _FxParent;
    [SerializeField] private Transform _FxSpawnPoint;
    [SerializeField] private Transform _alert;

    private int _value;
    private BuferItemController _buferItemController;

    [Inject]
    private void Construct(
        BuferItemController buferItemController
    )
    {
        _buferItemController = buferItemController;
    }

    private void OnEnable()
    {
        DrawLootValue();
    }

    public override void LocatorAction()
    {
        _addLootAnimation.Play();
        Instantiate(_Fxprefab, _FxSpawnPoint.position, Quaternion.identity, _FxParent);
        DrawLootValue();

    }

    private void DrawLootValue()
    {
        int itemInBuferCount = _buferItemController.Items.Count;

        if (itemInBuferCount > 0)
        {
            _alert.gameObject.SetActive(true);
        }
        else
        {
            _alert.gameObject.SetActive(false);
        }

        _text.text = itemInBuferCount.ToString();
    }
}