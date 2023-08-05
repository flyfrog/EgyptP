using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button _closeBut;
    [SerializeField] private Button _buyHero;
    [SerializeField] private BaseWindow _shop;


    private HeroController _heroController;

    [Inject]
    private void Construct(
        HeroController heroController
    )
    {
        _heroController = heroController;
    }

    private void Awake()
    {
        _closeBut.onClick.AddListener(HideShopWindow);
        _buyHero.onClick.AddListener(BuyHero);
    }



    private Hero GetHeroForSell()
    {
        foreach (var hero in _heroController.Heroes)
        {
            if (hero.Status == HeroStatus.Selling)
            {
                return hero;
            }
        }

        return null;
    }

    public bool HasHeroForSell()
    {
        if (GetHeroForSell() != null)
        {
            return true;
        }

        return false;
    }


    private void BuyHero()
    {
        _heroController.SetHeroUseStatus(GetHeroForSell());
        _shop.Hide();
    }

    private void HideShopWindow()
    {
        _shop.Hide();
    }
}