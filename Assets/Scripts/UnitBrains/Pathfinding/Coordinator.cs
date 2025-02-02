using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class Coordinator
{ 
    private static Coordinator _instance;
    private static int _count = 0;
    private IReadOnlyRuntimeModel _runtimemodel;
    private TimeUtil _timeutil;

    private Vector2Int _recTarget;
    private Vector2Int _recPoint;

    public Vector2Int RecTarget => _recTarget;
    public Vector2Int RecPoint => _recPoint;
    public int Count { get { return _count; } set { _count = value; } }

    private Coordinator() 
    {
        _runtimemodel = ServiceLocator.Get<IReadOnlyRuntimeModel>();
        _timeutil = ServiceLocator.Get<TimeUtil>();

        _recPoint = _runtimemodel.RoMap.Bases[RuntimeModel.BotPlayerId];
        _timeutil.AddFixedUpdateAction(CoordinatorUpdate);
    }
    public static Coordinator GetInstance()
    {
        if (_instance == null)
            _instance = new Coordinator();

        return _instance;
    }
    private void CoordinatorUpdate(float deltatime)
    {
        CalculateRecommendation();
    }
    private void CalculateRecommendation()
    {
        float middlePoint = (Vector2Int.Distance(_runtimemodel.RoMap.Bases[RuntimeModel.PlayerId], _runtimemodel.RoMap.Bases[RuntimeModel.BotPlayerId])) / 2;
        Vector2Int PosResult = new Vector2Int();
        Vector2Int HealthResult = new Vector2Int();
        float minDist = float.MaxValue;
        float minHealth = float.MaxValue;
        foreach ( var bot in _runtimemodel.RoBotUnits )
        {
            float dist = Vector2Int.Distance(_runtimemodel.RoMap.Bases[RuntimeModel.PlayerId], bot.Pos);
            if( dist < minDist)
            {
                minDist = dist;
                PosResult = bot.Pos;
            }
            if(bot.Health < minHealth)
            {
                HealthResult = bot.Pos;
            }
        }

        if(Vector2Int.Distance(_runtimemodel.RoMap.Bases[RuntimeModel.PlayerId], PosResult) < middlePoint)
        {
            _recTarget = PosResult;
            _recPoint = _runtimemodel.RoMap.Bases[RuntimeModel.PlayerId] + Vector2Int.right;
        }
        else
        { 
            _recTarget = HealthResult;
            _recPoint = PosResult;
        }
        Count = 0;

    }

}
