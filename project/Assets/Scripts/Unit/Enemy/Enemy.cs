using UnityEngine;
using Zenject;

public class Enemy : Unit
{
    private CharacterSettings _characterSettings;
    private GameManager _gameManager;
    private EnemySpawner _enemySpawner;
    private EnemyBrainBase _brainPrefab;
    protected TileTools _tileTools;
    protected WorldController _worldController;

    [Inject]
    public void Construct(
        GameManager gameManager,
        EnemySpawner enemySpawner,
        TileTools tileTools,
        WorldController worldController
    )
    {
        _enemySpawner = enemySpawner;
        _gameManager = gameManager;
        _tileTools = tileTools;
        _worldController = worldController;
        _gameManager.EndGame += EndGameDestroy;
    }

    public void SetBrain(EnemyBrainBase brain)
    {
        Debug.Log("SetBrain");
        _brainPrefab = Instantiate(brain, transform);
        _brainPrefab.Init(
            unit: this,
            unitGroundController: _unitGroundController,
            tileTools: _tileTools,
            worldController: _worldController
        );
    }

    protected override void InitForChildClass(CharacterSettings characterSettings)
    {
        Debug.Log("InitForChildClass");
    }

    public override void TouchWorldWall()
    {
        Suicide();
    }

    public override void KillEnemy()
    {
        Debug.Log("Enemy KillEnemy");
    }

    public override void DeadByOtherPlayer()
    {
        Suicide();
    }

    public override void DeadTogetherOtherPlayer()
    {
        Suicide();
    }

    protected override void Destroy()
    {
        _gameManager.EndGame -= EndGameDestroy;
    }

    protected override void Suicide()
    {
        _enemySpawner.NeedSpawn();
        Destroy(gameObject);
    }

    private void EndGameDestroy()
    {
        Destroy(gameObject);
    }
}