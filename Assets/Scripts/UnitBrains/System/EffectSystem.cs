using Assets.Scripts.UnitBrains.System;
using Model.Runtime;
using PlasticGui.WorkspaceWindow.PendingChanges;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public enum StatusType
{
    Buff,
    Debuff
}
public class EffectSystem : MonoBehaviour
{
    private Dictionary<Unit, IStatusEffect> _effectsStatus = new();
    private Coroutine _coroutine;
    public void UnitRegister(Unit unit)
    {
        _effectsStatus[unit] = new BuffEffects(BuffType.None);
    }
    public void AddEffect(Unit unit, StatusType type)
    {
        switch (type)
        {
            case StatusType.Buff:
                _effectsStatus[unit] = new BuffEffects();
                Debug.LogWarning($"AddEffect{_effectsStatus[unit].Name}");
                break;
            case StatusType.Debuff:
                _effectsStatus[unit] = new DebuffEffects();
                Debug.LogWarning($"AddEffect{_effectsStatus[unit].Name}");
                break;
                
        }
        if (_effectsStatus[unit].Name == "None") return;
        StartCoroutine(EffectLifetime(unit));

    }
    private IEnumerator EffectLifetime(Unit unit)
    {
        
        yield return new WaitForSeconds(_effectsStatus[unit].Duration);
        Debug.LogWarning($"BreakEffect{_effectsStatus[unit].Name}");
        RemoveEffect(unit);
    }
    
    private void RemoveEffect(Unit unit)
    {
        _effectsStatus[unit].BuffBreak();
    }
    public IStatusEffect GetStatus(Unit unit)
    {
        return _effectsStatus[unit];
    }
}
