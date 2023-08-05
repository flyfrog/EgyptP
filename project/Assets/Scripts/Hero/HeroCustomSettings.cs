using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "HeroCustomSettings", menuName = "Game/HeroCustomSettings")]
public class HeroCustomSettings: ScriptableObject
{
    public string Name;
    public HeroRarityType Rarity;
    public HeroView HeroView;
    public HeroUIView HeroUIView;
    public TrailColorGradient TrailColorGradient;
    public TileBase MyGroundTile;
    public HeroSkill BaseSkills;

    // сделать как с предметами что контролер держит всех героев сразу
    // и мы просто меняем им статус куплен они или нет?
    // тогда не нужно будет перекидывать им ссылки туда сюда между магазином и контролером
}