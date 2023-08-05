using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;
using Zenject;

public class InventorController : MonoBehaviour
{
    [SerializeField] private InventorSlot _slotPrefab;
    [SerializeField] private Transform _slotsParent;

    [SerializeField] private int _minRowsCount = 3;
    [SerializeField] private int _slotsInRow = 5;


    private List<InventorSlot> _slots = new();
    private ItemController _itemController;

    [Inject]
    private void Construct(
        ItemController itemController
    )
    {
        _itemController = itemController;
        _itemController.OnAdd += AddItemInInventor;
        _itemController.OnDelited += DeliteItemFromInventar;
        _itemController.OnStatusUpdate += ItemUpdate;
    }

    private void Start()
    {
        AddStartFreeSlot();
        DrawStartItems();
    }

    private void OnDestroy()
    {
        _itemController.OnAdd -= AddItemInInventor;
        _itemController.OnDelited -= DeliteItemFromInventar;
        _itemController.OnStatusUpdate -= ItemUpdate;
    }


    public void AddItemInInventor(Item item)
    {
        if (GetFreeSlots().Count == 0)
        {
            AddSlotsRow();
        }

        var slot = GetFreeSlots().First();
            slot.SetItem(item);
    }

    public void DeliteItemFromInventar(Item item)
    {
        var slot = GetSlotByItem(item);
        DeliteSlot(slot);
        AddSlot();
        DeliteFreeRowIfIsHas();
    }

    private void DrawStartItems()
    {
        foreach (var item in _itemController.Items)
        {
            if (item.ItemStatus == ItemStatus.InDressUnit)
            {
                continue;
            }

            AddItemInInventor(item);
        }
    }

    private void ItemUpdate(Item item)
    {

        if (item.ItemStatus == ItemStatus.InInventor)
        {
            AddItemInInventor(item);
            return;
        }

        if (item.ItemStatus == ItemStatus.InDressUnit)
        {
            DeliteItemFromInventar(item);
            return;
        }
    }

    private InventorSlot GetSlotByItem(Item item)
    {
        foreach (var slot in _slots)
        {
            if (slot.Item == item)
            {
                return slot;
            }
        }

        return null;
    }

    private void DeliteSlot(InventorSlot slot)
    {
        slot.DeliteSlot();
        _slots.Remove(slot);
    }

    private void AddStartFreeSlot()
    {
        for (int i = 0; i < _minRowsCount; i++)
        {
            AddSlotsRow();
        }
    }

    private void DeliteFreeRowIfIsHas()
    {
        int minSlotsCount = _slotsInRow * _minRowsCount;

        if (_slots.Count == minSlotsCount)
        {
            return;
        }

        if (GetFreeSlots().Count < _slotsInRow)
        {
            return;
        }

        if (GetFreeSlots().Count == _slotsInRow)
        {
            var freeSlotsForDestroy = GetFreeSlots();
            for (int i = 0; i < freeSlotsForDestroy.Count; i++)
            {
                DeliteSlot(freeSlotsForDestroy[i]);
            }
        }
    }


    private void AddSlotsRow()
    {
        for (int i = 0; i < _slotsInRow; i++)
        {
            AddSlot();
        }
    }

    private void AddSlot()
    {
        var slot = Instantiate(_slotPrefab, _slotsParent);
        _slots.Add(slot);
    }


    private List<InventorSlot> GetFreeSlots()
    {
        List<InventorSlot> freerSlots = new();

        foreach (var slot in _slots)
        {
            if (slot.Item == null)
            {
                freerSlots.Add(slot);
            }
        }

        return freerSlots;
    }
}