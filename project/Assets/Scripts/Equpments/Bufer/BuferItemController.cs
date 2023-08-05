using System.Collections.Generic;
using Zenject;

public class BuferItemController
{

    public List<Item> Items => _items;
    private List<Item> _items = new ();

    private ItemController _itemController;

    [Inject]
    public BuferItemController(
        ItemController itemController
        )
    {
        _itemController = itemController;
    }



    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public void ClearBufer()
    {
        _items.Clear();
    }

    public void SendBuferItemsInToInventor()
    {
        foreach (var item in _items)
        {
            _itemController.AddItem(item);
        }

        _items.Clear();
    }

}