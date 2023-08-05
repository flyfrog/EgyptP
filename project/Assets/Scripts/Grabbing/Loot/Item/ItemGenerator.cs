using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private Item _testItem;
    public Item GetRandomItem()
    {
        Item newItem = new()
        {
            uiView = _testItem.uiView,
            Rarity = _testItem.Rarity,
            Level = _testItem.Level,
            Name = _testItem.Name,
            Cost = _testItem.Cost
        };

        return newItem;
    }

}