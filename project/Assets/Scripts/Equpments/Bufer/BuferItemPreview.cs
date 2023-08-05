using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuferItemPreview : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotSpawnParent;
    private BuferItemController _buferItemController;
    private List<GameObject> _slots = new();
 

    [Inject]
    private void Construct(
        BuferItemController buferItemController
    )
    {
        _buferItemController = buferItemController;
    }

    private void OnEnable()
    {
        ShowBufetItem();
    }

    private void OnDisable()
    {
        DeliteBuferItems();
    }

    private void ShowBufetItem()
    {
        var items = _buferItemController.Items;

        foreach (var item in items)
        {
            SpawnSlotWithItem(item);
        }
    }

    private void SpawnSlotWithItem(Item item)
    {
        var slot = Instantiate(_slotPrefab.gameObject, _slotSpawnParent);
        Instantiate(item.uiView, slot.transform);
        _slots.Add(slot);
    }


    private void DeliteBuferItems()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
    }
}