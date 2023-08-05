using System;
using UnityEngine;

public class ContactService : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ILootable loot))
        {
            if (_unit.Player)
            {
                loot.StartLooting();
                return;
            }

            //взял не игрок
            loot.LootingForEnemy();
            return;
        }

        if (other.TryGetComponent(out DamageCarier damage))
        {
            _unit.TouchWorldWall();
            return;
        }


        if (other.TryGetComponent(out TrailAgent trailAgent))
        {
            if (trailAgent.Unit == _unit)
            {
                return;
            }

            trailAgent.Unit.DeadByOtherPlayer();
            return;
        }
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Unit otherUnit))
        {

            //тут нужно проверить обоих и выбрать это для кого смерть или для обоих
            var myExploring = _unit.ExploringStatus;
            var otherExploring = otherUnit.ExploringStatus;

            if (myExploring==false && otherExploring==true)
            {
                otherUnit.DeadByOtherPlayer();
            }

            if (myExploring==true && otherExploring==true)
            {
                otherUnit.DeadTogetherOtherPlayer();
                _unit.DeadTogetherOtherPlayer();
            }

            if (myExploring==true && otherExploring==false)
            {
                _unit.DeadByOtherPlayer();
            }
            return;
        }
    }
}