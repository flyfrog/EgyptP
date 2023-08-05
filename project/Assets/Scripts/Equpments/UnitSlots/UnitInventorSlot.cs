using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[Serializable]
public class UnitInventorSlot : MonoBehaviour
{
    [field: SerializeField] public int Id { get; private set; }
    public UnitSlotStatus Status { get; private set; }
    public Item Item { get; private set; }

    [SerializeField] private Transform SlotParent;
    [SerializeField] private Transform LockIcon;
    [SerializeField] private Transform EmptyIcon;
    [field: SerializeField] public int UnlockLevel { get; private set; }

    [SerializeField] private Button _button;
    private InventorItemInfoController _inventorItemInfoController;
    private ItemUIView _itemUIView;


    [Inject]
    private void Construct(
        InventorItemInfoController inventorItemInfoController
    )
    {
        _inventorItemInfoController = inventorItemInfoController;
    }

    private void Awake()
    {
        if (_button!=null)
        {
            _button.onClick.AddListener(ShowItemInfoWindow);
        }
    }


    private void ShowItemInfoWindow()
    {
        if (Item == null)
        {
            return;
        }

        if (Status == UnitSlotStatus.Lock)
        {
            //тут код инфо о том что нужно разблокировать ячейку
            return;
        }

        _inventorItemInfoController.ShowInfoForItem(Item);
    }


    public void SetItem(Item item)
    {
        Item = item;
        _itemUIView = Instantiate(Item.uiView, SlotParent);
        Status = UnitSlotStatus.Full;
        LockIcon.gameObject.SetActive(false);
        EmptyIcon.gameObject.SetActive(false);
    }

    public void DeliteCurrentItem()
    {
        if (_itemUIView != null)
        {
            _itemUIView.DestroyItSelf();
            Item = null;
        }

        UnlockSlot();
    }

    public void LockSlot()
    {
        LockIcon.gameObject.SetActive(true);
        EmptyIcon.gameObject.SetActive(false);
        Status = UnitSlotStatus.Lock;
    }

    public void UnlockSlot()
    {
        LockIcon.gameObject.SetActive(false);
        EmptyIcon.gameObject.SetActive(true);
        Status = UnitSlotStatus.Empty;
    }
}