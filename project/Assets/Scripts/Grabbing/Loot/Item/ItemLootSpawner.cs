using MyBox;
using UnityEngine;
using Zenject;

public class ItemLootSpawner : MonoBehaviour
{
    [SerializeField] private int _countLoot = 100;

    public ItemLootView _prefab;
    private GameManager _gameManager;
    private WorldController _worldController;
    private TileTools _tileTools;
    private ItemGenerator _itemGenerator;

    [Inject]
    private void Construct(
        TileTools tileTools,
        GameManager gameManager,
        WorldController worldController,
        ItemGenerator itemGenerator
    )
    {
        _gameManager = gameManager;
        _worldController = worldController;
        _tileTools = tileTools;
        _itemGenerator = itemGenerator;
    }


    public void SpawnLoot()
    {
        var countLoot = _countLoot;
        WorldLimits limits = _worldController.WorldLimits;

        for (int i = 0; i < countLoot; i++)
        {
            float xPos = Random.Range(limits.minX, limits.maxX);
            float yPos = Random.Range(limits.minY, limits.maxY);


            Vector2 pos = new Vector2(xPos, yPos);

            var lootGO = Instantiate(_prefab.gameObject, pos, Quaternion.identity, transform);


            lootGO.GetComponent<BaseLoot>().SetCellPosition(_tileTools.GetGridPositionForWorldPoint(pos));


            lootGO.GetComponent<ItemLootView>().SetItem(_itemGenerator.GetRandomItem());
        }
    }
}