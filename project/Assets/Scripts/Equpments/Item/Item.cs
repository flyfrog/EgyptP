using System;
using UnityEngine;

[Serializable]
public class Item
{
    public ItemUIView uiView;
    public ItemRarityType Rarity;
    public int Level;
    [HideInInspector] public ItemStatus ItemStatus;
    public string Name;
    public string Description;
    [HideInInspector] public Hero Hero;
    [HideInInspector] public int HeroSlotId;
    public int Cost=1;
    [HideInInspector] public bool IsNew = true;
    public HeroSkill Skills = new ();
}