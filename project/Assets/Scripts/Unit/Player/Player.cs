using System;
using UnityEngine;
using Zenject;

public class Player : Unit
{
    [SerializeField] private PlayerRotation playerRotation;
    private GameManager _gameManager;

    [Inject]
    public void Construct(
        CameraFollow cameraFollow,
        GameManager gameManager
    )
    {
        _gameManager = gameManager;
        BindCameraToMe(cameraFollow);
    }

    protected override void InitForChildClass(CharacterSettings characterSettings)
    {
        playerRotation.Init(
            unit: _unit
            );
    }


    public override void TouchWorldWall()
    {
        Debug.Log("TouchWorldWall");
        Suicide();
    }

    public override void KillEnemy()
    {
        Debug.Log("Player KillEnemy");
    }

    public override void DeadByOtherPlayer()
    {
        Debug.Log("DeadByOtherPlayer");
        Suicide();
    }

    public override void DeadTogetherOtherPlayer()
    {
        Debug.Log("DeadTogetherOtherPlayer");
        Suicide();
    }

    protected override void Destroy()
    {
    }


    protected override void Suicide()
    {
        _gameManager.Loose();
        Destroy(gameObject);
    }

    private void BindCameraToMe(CameraFollow cameraFollow)
    {
        cameraFollow.Target = transform;
    }
}