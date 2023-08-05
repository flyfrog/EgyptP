using System;
using UnityEngine;

[Serializable]
public struct LevelProgress
{
    public int CurentLevel;
    public int CurentExpa;
    public int[] ExpaForLevels;

    public int MaxLevel => GetMaxLevel();
    public int ExpaForNextLevel => GetExpaValueForSwitchNextLevel();

    private int GetMaxLevel()
    {
        return ExpaForLevels.Length - 1;
    }

    public int GetExpaValueForSwitchNextLevel()
    {
        return ExpaForLevels[CurentLevel];
    }


}