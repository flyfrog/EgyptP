using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitSlotsController : MonoBehaviour
{
    [SerializeField] private UnitInventorSlot[] _unitSlots;
    [SerializeField] private UnitInventorSlot[] _previwUnitSlots;

    public bool HasFreeSlot => CheckHasFreeSlots();
    private HeroController _heroController;
    private ItemController _itemController;


    [Inject]
    private void Construct(
        HeroController heroController,
        ItemController itemController
    )
    {
        _heroController = heroController;
        _heroController.OnHeroChanged += DrawSlotsForUnit;
        _itemController = itemController;
        _itemController.OnDelited += TryDeliteItem;
        _itemController.OnStatusUpdate += TryMoveItem;

    }

    private void Start()
    {
        DrawSlotsForUnit(_heroController.CurentHero);
    }

    private void OnDestroy()
    {
        _heroController.OnHeroChanged -= DrawSlotsForUnit;
        _itemController.OnDelited -= TryDeliteItem;
        _itemController.OnStatusUpdate -= TryMoveItem;
    }

    public UnitInventorSlot GetFreeSlot()
    {
        foreach (var slot in _unitSlots)
        {
            if (slot.Status == UnitSlotStatus.Empty)
            {
                return slot;
            }
        }

        return null;
    }

    private void TryMoveItem(Item item)
    {
        if (item.ItemStatus == ItemStatus.InInventor)
        {
            var slot = GetSlotByIItem(item);
            slot.DeliteCurrentItem();
            _previwUnitSlots[Array.IndexOf(_unitSlots,slot)].DeliteCurrentItem();
            return;
        }

        if (item.ItemStatus == ItemStatus.InDressUnit)
        {
            var slot = GetSlotById(item.HeroSlotId);
            slot.SetItem(item);
            _previwUnitSlots[Array.IndexOf(_unitSlots,slot)].SetItem(item);
        }
    }

    private UnitInventorSlot GetSlotByIItem(Item item)
    {
        foreach (var slot in _unitSlots)
        {
            if (slot.Item == item)
            {
                return slot;
            }
        }

        return null;
    }

    private void TryDeliteItem(Item item)
    {
        var slot = GetSlotByIItem(item);
        if (slot == null)
        {
            return;
        }

        slot.DeliteCurrentItem();
        _previwUnitSlots[Array.IndexOf(_unitSlots, slot)].DeliteCurrentItem();
    }


    private void DrawSlotsForUnit(Hero hero)
    {
        ResetAllSlots();
        LockSlotsForHero(hero);

        var heroItems = GetHeroItems(hero);
        foreach (var item in heroItems)
        {
            var slot = GetSlotById(item.HeroSlotId);
            slot.SetItem(item);
            _previwUnitSlots[Array.IndexOf(_unitSlots,slot)].SetItem(item);
        }
    }

    private UnitInventorSlot GetSlotById(int id)
    {
        foreach (var slot in _unitSlots)
        {
            if (slot.Id == id)
            {
                return slot;
            }
        }

        return null;
    }

    private List<Item> GetHeroItems(Hero hero)
    {
        List<Item> items = new();
        foreach (var item in _itemController.Items)
        {
            if (item.Hero == hero)
            {
                items.Add(item);
            }
        }

        return items;
    }

    private bool CheckHasFreeSlots()
    {
        if (GetFreeSlot() != null)
        {
            return true;
        }

        return false;
    }

    private void LockSlotsForHero(Hero hero)
    {
        foreach (var slot in _unitSlots)
        {
            if (slot.UnlockLevel > hero.LevelProgress.CurentLevel)
            {
                slot.LockSlot();
                _previwUnitSlots[Array.IndexOf(_unitSlots,slot)].LockSlot();
                continue;
            }
        }
    }

    private void ResetAllSlots()
    {
        foreach (var slot in _unitSlots)
        {
            slot.DeliteCurrentItem();
            _previwUnitSlots[Array.IndexOf(_unitSlots,slot)].DeliteCurrentItem();
        }
    }
}