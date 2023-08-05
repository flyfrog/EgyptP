using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item test;

    public event Action<Item> OnDelited;
    public event Action<Item> OnStatusUpdate;
    public event Action<Item> OnViewedUpdate;
    public event Action<Item> OnAdd;

    public List<Item> Items => _items;
    [SerializeField] private List<Item> _items;


    public void DeliteItem(Item item)
    {
        OnDelited?.Invoke(item);
        _items.Remove(item);
    }


    public void AddItem(Item item)
    {
        _items.Add(item);
        OnAdd?.Invoke(item);
    }

    public void UpdateViewedStatus(Item item)
    {
        OnViewedUpdate?.Invoke(item);
    }

    public void UpdateItemStatus(Item item)
    {
        OnStatusUpdate?.Invoke(item);
    }

    [MyBox.ButtonMethod()]
    public void AddTesmItem()
    {
        Item item = new Item()
        {
            uiView = test.uiView,
            Name =  test.Name,
        };

        AddItem(item);
    }


    [MyBox.ButtonMethod()]
    public void TestDeliteItem()
    {
        var item = _items.First();
        DeliteItem(item);
    }

    public List<Item> GetItemsForHero(Hero hero)
    {
        List<Item> items = new List<Item>();

        foreach (var item in _items)
        {
            if (item.Hero == hero && item.ItemStatus == ItemStatus.InDressUnit)
            {
                items.Add(item);
            }

        }

        return items;

    }



}