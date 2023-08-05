using UnityEngine;
using Zenject;

public class ChestSpawner : MonoBehaviour
{
    public BoxLoot _prefab;
    [SerializeField] private int _countBox = 50;


    private TileTools _tileTools;
    private GameManager _gameManager;
    private WorldController _worldController;

    [Inject]
    private void Construct(
        TileTools tileTools,
        GameManager gameManager,
        WorldController worldController
    )
    {
        _gameManager = gameManager;
        _tileTools = tileTools;
        _worldController = worldController;
    }

    public void SpawnBoxes()
    {
        WorldLimits limits = _worldController.WorldLimits;
        int countBoxes = _countBox;

        for (int i = 0; i < countBoxes; i++)
        {
            float xPos = Random.Range(limits.minX, limits.maxX);
            float yPos = Random.Range(limits.minY, limits.maxY);


            Vector2 pos = new Vector2(xPos, yPos);
            var box = Instantiate(_prefab, pos, Quaternion.identity, transform);
            box.SetCellPosition(_tileTools.GetGridPositionForWorldPoint(pos));
        }
    }
}