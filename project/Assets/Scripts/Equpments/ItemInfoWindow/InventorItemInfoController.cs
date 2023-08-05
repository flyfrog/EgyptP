using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorItemInfoController : MonoBehaviour
{
    [SerializeField] private BaseWindow _itemInfoWindow;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private Button _closeBut;
    [SerializeField] private Button _closeBackgroundBut;

    [SerializeField] private Button _sell;
    [SerializeField] private TextMeshProUGUI _sellCostText;

    [SerializeField] private Button _dress;
    [SerializeField] private Transform _dressTransform;

    [SerializeField] private Button _undressDress;
    [SerializeField] private Transform _unDressTransform;

    [SerializeField] private Button _noFreeSlots;
    [SerializeField] private Transform _noFreeSlotsTransfom;

    [SerializeField] private Button _levelSmallThenNeedBut;
    [SerializeField] private Transform _levelSmallThenNeedTransfom;

    [SerializeField] private Transform _iconSpawnParent;
    [SerializeField] private ItemTypeStyleSlot[] _itemTypeStyleSlots;

    [Serializable]
    public class RarytySlot
    {
        public Transform Label;
        public ItemRarityType rarityRarityType;
    }

    [SerializeField] private RarytySlot[] _rarytySlots;


    private Item _currentItem;
    private ItemUIView _currentItemUIView;
    private HeroController _heroController;
    private ItemController _itemController;
    private UnitSlotsController _unitSlotsController;

    [Inject]
    private void Construct(
        HeroController heroController,
        ItemController itemController,
        UnitSlotsController unitSlotsController
    )
    {
        _heroController = heroController;
        _itemController = itemController;
        _unitSlotsController = unitSlotsController;
    }

    private void Awake()
    {
        _closeBut.onClick.AddListener(Close);
        _closeBackgroundBut.onClick.AddListener(Close);

        _sell.onClick.AddListener(SellItem);
        _dress.onClick.AddListener(SetItemToHero);
        _undressDress.onClick.AddListener(Undress);
        _noFreeSlots.onClick.AddListener(NoFreeSlots);
    }


    public void ShowInfoForItem(Item item)
    {
        _currentItem = item;
        _itemInfoWindow.Show();
        SpawnItemView(_currentItem);
        DrawStyleForTypeItem(_currentItem);
        DrawName(_currentItem);
        ShowActionButtonForItem(_currentItem);
        DrawSellCost(_currentItem);
        DrawRarity(_currentItem);
        SetViewedStatus(_currentItem);
    }

    private void SetViewedStatus(Item item)
    {
        if (item.IsNew == true)
        {
            item.IsNew = false;
            _itemController.UpdateViewedStatus(item);
        }
    }

    private void DrawSellCost(Item item)
    {
        _sellCostText.text = item.Cost.ToString();
    }

    private void ShowActionButtonForItem(Item item)
    {
        HideAllVariantsButton();

        if (item.ItemStatus == ItemStatus.InInventor)
        {
            if (_heroController.CurentHero.LevelProgress.CurentLevel < item.Level)
            {
                _levelSmallThenNeedTransfom.gameObject.SetActive(true);
                return;
            }

            if (_unitSlotsController.GetFreeSlot() == null)
            {
                _noFreeSlotsTransfom.gameObject.SetActive(true);
                return;
            }

            _dressTransform.gameObject.SetActive(true);
            return;
        }

        if (item.ItemStatus == ItemStatus.InDressUnit)
        {
            _unDressTransform.gameObject.SetActive(true);
            return;
        }
    }

    private void HideAllVariantsButton()
    {
        _dressTransform.gameObject.SetActive(false);
        _unDressTransform.gameObject.SetActive(false);
        _noFreeSlotsTransfom.gameObject.SetActive(false);
        _levelSmallThenNeedTransfom.gameObject.SetActive(false);
    }


    private void DrawName(Item item)
    {
        _name.text = item.Name;
    }


    private void SpawnItemView(Item item)
    {
        _currentItemUIView = Instantiate(item.uiView, _iconSpawnParent);
    }

    private void DrawStyleForTypeItem(Item item)
    {
        ResetStyleSpecialByTypeSlots();

        foreach (var styleSlot in _itemTypeStyleSlots)
        {
            if (styleSlot.rarityType == item.Rarity)
            {
                ActivatianTransform(styleSlot.Transforms, true);
            }
        }
    }


    private void ResetStyleSpecialByTypeSlots()
    {
        foreach (var styleSlot in _itemTypeStyleSlots)
        {
            ActivatianTransform(styleSlot.Transforms, false);
        }
    }

    private void ActivatianTransform(Transform[] transforms, bool activateStatus)
    {
        foreach (var transform in transforms)
        {
            transform.gameObject.SetActive(activateStatus);
        }
    }


    private void Close()
    {
        _currentItemUIView.DestroyItSelf();
        _itemInfoWindow.Hide();
    }


    private void NoFreeSlots()
    {
        Close();
    }

    private void SellItem()
    {
        _itemController.DeliteItem(_currentItem);
        Close();
        //начислить деньги
    }

    private void SetItemToHero()
    {
        _currentItem.Hero = _heroController.CurentHero;
        _currentItem.HeroSlotId = _unitSlotsController.GetFreeSlot().Id;
        _currentItem.ItemStatus = ItemStatus.InDressUnit;
        _itemController.UpdateItemStatus(_currentItem);
        Close();
    }

    private void Undress()
    {
        _currentItem.Hero = null;
        _currentItem.HeroSlotId = 0;
        _currentItem.ItemStatus = ItemStatus.InInventor;
        _itemController.UpdateItemStatus(_currentItem);
        Close();
    }

    private void DrawRarity(Item item)
    {
        foreach (var slot in _rarytySlots)
        {
            if (slot.rarityRarityType == item.Rarity)
            {
                slot.Label.gameObject.SetActive(true);
                continue;
            }

            slot.Label.gameObject.SetActive(false);
        }
    }
}