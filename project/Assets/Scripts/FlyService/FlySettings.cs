using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FlySettings", order = 1)]
public class FlySettings : ScriptableObject
{
    public AnimationCurve XShiftCurve = AnimationCurve.Linear(0, 0, 1, 0);
    public AnimationCurve FlyDynamicCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float ShiftXMultiplier = 50;
    public float FlySpeed = 3f;

}

