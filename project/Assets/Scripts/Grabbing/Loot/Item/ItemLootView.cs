using System;
using UnityEngine;
using Zenject;

public class ItemLootView : MonoBehaviour
{
    [SerializeField] private Loot _loot;
    private Item _item;
    private BuferItemController _buferItemController;

    [Inject]
    private void Construct(
        BuferItemController buferItemController
    )
    {
        _buferItemController = buferItemController;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    private void Awake()
    {
        _loot.OnLootingStarted += FinishAction;
    }

    private void OnDestroy()
    {
        _loot.OnLootingStarted -= FinishAction;
    }

    private void FinishAction()
    {
        _buferItemController.AddItem(_item);
    }
}