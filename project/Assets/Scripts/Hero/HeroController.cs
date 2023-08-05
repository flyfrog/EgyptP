using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class HeroController : MonoBehaviour
{
    public event Action<Hero> OnUpdatedExpaCurrentHero;
    public event Action<Hero> OnHeroChanged;
    public event Action<Hero> OnLevelUp;

    public Hero CurentHero { get; private set; }
    public int AvailibleHeroisCount => _availibleHeroes.Count;
    public List<Hero> AvailibleHeroes => _availibleHeroes;
    public List<Hero> Heroes => _heroes;

    [SerializeField] private LevelProgress _defaultLevelProgress;
    [SerializeField] private HeroCustomSettings[] _customSettings;

    private List<Hero> _heroes;
    private int _heroPointer = 0;
    private List<Hero> _availibleHeroes;

    private void Awake()
    {
        _heroes = PrepareHeroes(_customSettings, _defaultLevelProgress);
        _heroes.First().Status = HeroStatus.Use; // только для теста потом решить проблему что SO сохраняется в плеймоде
        _availibleHeroes = MakeAvailibleHeroesList(_heroes);
        CurentHero = _heroes.First();
    }

    public void SelectHero(Hero hero)
    {
        CurentHero = hero;
        OnHeroChanged?.Invoke(CurentHero);
    }

    public void NextHero()
    {
        if (_heroPointer < _availibleHeroes.Count - 1)
        {
            _heroPointer++;
        }
        else
        {
            _heroPointer = 0;
        }

        SelectHero(_availibleHeroes[_heroPointer]);
    }

    public void PrevHero()
    {
        if (_heroPointer > 0)
        {
            _heroPointer--;
        }
        else
        {
            _heroPointer = _availibleHeroes.Count - 1;
        }

        SelectHero(_availibleHeroes[_heroPointer]);
    }


    public void AddExpaForCurrentHero(int expa)
    {
        var currentlevel = CurentHero.LevelProgress.CurentLevel;

        if (currentlevel == CurentHero.LevelProgress.MaxLevel)
        {
            return;
        }

        var expaForFinishlevel = CurentHero.LevelProgress.ExpaForNextLevel;
        CurentHero.LevelProgress.CurentExpa += expa;

        if (CurentHero.LevelProgress.CurentExpa >= expaForFinishlevel)
        {
            // опыта достаточно для перехода на сл уровень
            CurentHero.LevelProgress.CurentExpa -= expaForFinishlevel;
            CurentHero.LevelProgress.CurentLevel++;
            OnLevelUp?.Invoke(CurentHero);
            return;
        }

        // добавить каждому герою суперспособность по достижению 15 уровня.
        // Чтобы игрок мог ей пользоватьс
        //    При прокачке уровня способности героя не растут только предметами можно усилить это
        // суперспособности влияют только на добычу золота и предметов
        OnUpdatedExpaCurrentHero?.Invoke(CurentHero);
    }

    public void SetHeroUseStatus(Hero newHero)
    {
        newHero.Status = HeroStatus.Use;
        _availibleHeroes = MakeAvailibleHeroesList(_heroes);
        _heroPointer = _availibleHeroes.IndexOf(newHero);
        SelectHero(_availibleHeroes[_heroPointer]);
    }

    private List<Hero> MakeAvailibleHeroesList(List<Hero> heroes)
    {
        List<Hero> availibleHeroes = new List<Hero>();

        foreach (var hero in heroes)
        {
            if (hero.Status == HeroStatus.Use)
            {
                availibleHeroes.Add(hero);
            }
        }

        return availibleHeroes;
    }

    private List<Hero> PrepareHeroes(HeroCustomSettings[] customSettings, LevelProgress defaultLevelProgress)
    {
        var heroes = new List<Hero>();

        foreach (var custom in customSettings)
        {
            Hero newHero = new()
            {
                Custom = custom,
                LevelProgress = defaultLevelProgress,
                Status = HeroStatus.Selling
            };
            heroes.Add(newHero);
        }

        return heroes;
    }
}