using System;
using TMPro;
using UnityEngine;
using Zenject;

public class NewItemCountPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _newItemCount;
    [SerializeField] private Transform _newItemLabel;
    private ItemController _itemController;


    [Inject]
    private void Construct(
        ItemController itemController
    )
    {
        _itemController = itemController;
        _itemController.OnAdd += DrawNewItemCount;
        _itemController.OnViewedUpdate += DrawNewItemCount;
    }

    private void Start()
    {
        DrawNewItemCount(null);
    }

    private void OnDestroy()
    {
        _itemController.OnAdd -= DrawNewItemCount;
        _itemController.OnViewedUpdate -= DrawNewItemCount;
    }


    private void DrawNewItemCount(Item i)
    {
        int newItemCount = MathNewItemCount();

        if (newItemCount == 0)
        {
            _newItemLabel.gameObject.SetActive(false);
            return;
        }

        _newItemLabel.gameObject.SetActive(true);
        _newItemCount.text = newItemCount.ToString();
    }

    private int MathNewItemCount()
    {
        int count = 0;

        foreach (var item in _itemController.Items)
        {
            if (item.IsNew)
            {
                count++;
            }
        }

        return count;
    }
}