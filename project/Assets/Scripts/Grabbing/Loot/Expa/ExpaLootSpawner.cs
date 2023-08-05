using UnityEngine;
using Zenject;

public class ExpaLootSpawner : MonoBehaviour
{
    [SerializeField] private int _countLoot = 100;

    public ExpaLootView _prefab;

    private WorldController _worldController;
    private TileTools _tileTools;

    [Inject]
    private void Construct(
        TileTools tileTools,
        WorldController worldController
    )
    {
        _worldController = worldController;
        _tileTools = tileTools;
    }


    public void SpawnExpa()
    {
        var countLoot = _countLoot;
        WorldLimits limits = _worldController.WorldLimits;

        for (int i = 0; i < countLoot; i++)
        {
            float xPos = Random.Range(limits.minX, limits.maxX);
            float yPos = Random.Range(limits.minY, limits.maxY);


            Vector2 pos = new Vector2(xPos, yPos);

            var lootGO = Instantiate(_prefab.gameObject, pos, Quaternion.identity, transform);

            lootGO.GetComponent<ExpaLootView>().SetExpaValue(2);
            lootGO.GetComponent<BaseLoot>().SetCellPosition(_tileTools.GetGridPositionForWorldPoint(pos));

        }
    }
}