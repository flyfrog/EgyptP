using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Hero
{
    [HideInInspector] public LevelProgress LevelProgress;
    public HeroCustomSettings Custom;
    public HeroStatus Status;
    // сделать как с предметами что контролер держит всех героев сразу
    // и мы просто меняем им статус куплен они или нет?
    // тогда не нужно будет перекидывать им ссылки туда сюда между магазином и контролером
}