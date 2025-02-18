using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    string Name { get; }
    float Duration { get; }

    float AttackSpeedModifier {  get; }
    float MoveModifier { get; }

    public void BuffBreak() { }
}
