using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventorSlot : MonoBehaviour
{
    [SerializeField] private Button _button;

    private ItemUIView _itemUIView;
    public Item Item { get; private set; }
    private InventorItemInfoController _inventorItemInfoController;

    private ItemController _itemController;

    [Inject]
    private void Construct(
        InventorItemInfoController inventorItemInfoController,
        ItemController itemController
    )
    {
        _inventorItemInfoController = inventorItemInfoController;
        _itemController = itemController;
        _itemController.OnViewedUpdate += UpdateViewedStatus;
    }

    private void Awake()
    {
        _button.onClick.AddListener(ShowItemInfoWindow);
    }

    private void OnDestroy()
    {
        _itemController.OnViewedUpdate -= UpdateViewedStatus;
    }


    private void ShowItemInfoWindow()
    {
        if (Item == null)
        {
            return;
        }

        _inventorItemInfoController.ShowInfoForItem(Item);
    }

    private void UpdateViewedStatus(Item item)
    {
        if (item == Item)
        {
            _itemUIView.SetViewedStatus(Item.IsNew);
        }
    }


    public void SetItem(Item item)
    {
        Item = item;
        _itemUIView = Instantiate(Item.uiView, transform);
            _itemUIView.SetViewedStatus(Item.IsNew);
    }

    public void DeliteSlot()
    {
        if (_itemUIView != null)
        {
            _itemUIView.DestroyItSelf();
            Item = null;
        }

        Destroy(gameObject);
    }
}