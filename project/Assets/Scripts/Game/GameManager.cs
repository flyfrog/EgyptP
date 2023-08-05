using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _gameUI;
    [SerializeField] private Transform _lobbyUI;

    [SerializeField] private BaseWindow _buferItemWindow;
    [SerializeField] private BaseWindow _resultWindow;


    [SerializeField] private Button _cancelGrabBuferItems;
    [SerializeField] private Button _TakeBuferItems;

    [SerializeField] private Button _grabRewards;

    public event Action StartGame;
    public event Action EndGame;


    private WorldController _worldController;
    private ItemLootSpawner _itemLootSpawner;
    private ChestSpawner _chestSpawner;
    private PlayerSpawner _playerSpawner;
    private EnemySpawner _enemySpawner;
    private Loading _loading;
    private BuferItemController _buferItemController;
    private ExpaLootSpawner _expaLootSpawner;

    private void Awake()
    {
        _cancelGrabBuferItems.onClick.AddListener(CancelGrabBuferItem);
        _grabRewards.onClick.AddListener(GrabRewards);
        _TakeBuferItems.onClick.AddListener(TakeBuferItems);
    }

    [Inject]
    private void Construct(
        WorldController worldController,
        ItemLootSpawner itemLootSpawner,
        ChestSpawner chestSpawner,
        PlayerSpawner playerSpawner,
        EnemySpawner enemySpawner,
        Loading loading,
        BuferItemController buferItemController,
        ExpaLootSpawner expaLootSpawner
    )
    {
        _worldController = worldController;
        _itemLootSpawner = itemLootSpawner;
        _chestSpawner = chestSpawner;
        _playerSpawner = playerSpawner;
        _enemySpawner = enemySpawner;
        _loading = loading;
        _buferItemController = buferItemController;
        _expaLootSpawner = expaLootSpawner;
    }

    private void Start()
    {
        _worldController.MakeGameZone();
    }

    public void StartClassicGame()
    {
        _gameUI.gameObject.SetActive(true);
        _lobbyUI.gameObject.SetActive(false);

        _loading.OnLoadingEnded += StartCoreAfterLoading;
        _loading.StartLoading();
    }

    private void StartCoreAfterLoading()
    {
        _loading.OnLoadingEnded -= StartCoreAfterLoading;
        StartGame?.Invoke();
        _itemLootSpawner.SpawnLoot();
        _chestSpawner.SpawnBoxes();
        _playerSpawner.Spawn();
        _expaLootSpawner.SpawnExpa();
        _enemySpawner.FirstSpawn();
    }

    public void Win()
    {
        EndGame?.Invoke();
        _worldController.Clear();
    }

    public void Loose()
    {
        Debug.Log("Loose");
        _buferItemWindow.Show();
    }

    private void TakeBuferItems()
    {
        _buferItemWindow.Hide();
        _resultWindow.Show();
        _buferItemController.SendBuferItemsInToInventor();
    }

    private void CancelGrabBuferItem()
    {
        _buferItemWindow.Hide();
        _resultWindow.Show();
    }


    private void GrabRewards()
    {
        _resultWindow.Hide();
        _gameUI.gameObject.SetActive(false);
        _lobbyUI.gameObject.SetActive(true);
        ClearGameFielw();
    }

    private void ClearGameFielw()
    {
        EndGame?.Invoke();
        _worldController.Clear();
        _enemySpawner.StopSpawning();
    }
}