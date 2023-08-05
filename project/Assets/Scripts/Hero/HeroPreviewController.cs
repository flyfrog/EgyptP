using System;
using UnityEngine;
using Zenject;

public class HeroPreviewController : MonoBehaviour
{
    [SerializeField] private Transform _heroParent;
    [SerializeField] private HeroUIView _curentHeroView;

    private HeroController _heroController;
    private Hero _curentHero;


    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
        _heroController.OnHeroChanged += UpdateHero;
    }

    private void Start()
    {
        UpdateHero(_heroController.CurentHero);
    }

    private void UpdateHero(Hero hero)
    {
        if (_curentHero == hero)
        {
            return;
        }

        _curentHeroView.DestroyItSelf();

        _curentHero = hero;
        _curentHeroView = Instantiate(hero.Custom.HeroUIView, _heroParent);
    }
}