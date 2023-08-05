using System;
using UnityEngine;
using Zenject;

public class HeroInfoRarityTypePreview : MonoBehaviour
{
    [Serializable]
    public class RarytySlot
    {
        public Transform Label;
        public HeroRarityType rarityRarityType;
    }

    [SerializeField] private RarytySlot[] _slots;


    private HeroController _heroController;

    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
        _heroController.OnHeroChanged += Refresh;
    }

    private void Start()
    {
        Refresh(_heroController.CurentHero);
    }

    private void Refresh(Hero hero)
    {
        foreach (var slot in _slots)
        {
            if (slot.rarityRarityType == hero.Custom.Rarity)
            {
                slot.Label.gameObject.SetActive(true);
                continue;
            }

            slot.Label.gameObject.SetActive(false);
        }

    }


}