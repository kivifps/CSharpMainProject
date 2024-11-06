using System.Collections.Generic;
using System.Linq;
using GluonGui.Dialog;
using Model;
using Model.Runtime.Projectiles;
using UnityEngine;
using Utilities;

namespace UnitBrains.Player
{
    public class SecondUnitBrain : DefaultPlayerUnitBrain
    {
        public override string TargetUnitName => "Cobra Commando";
        private const float OverheatTemperature = 3f;
        private const float OverheatCooldown = 2f;
        private float _temperature = 0f;
        private float _cooldownTime = 0f;
        private bool _overheated;

        List<Vector2Int> outOfReachTarget = new List<Vector2Int>();

        protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
        {
            float overheatTemperature = OverheatTemperature;
            ///////////////////////////////////////
            // Homework 1.3 (1st block, 3rd module)
            ///////////////////////////////////////           
            if (GetTemperature() >= overheatTemperature) return;
            else
            {
                IncreaseTemperature();
                for (int i = 0; i < GetTemperature(); i++) 
                {
                    var projectile = CreateProjectile(forTarget);
                    AddProjectileToList(projectile, intoList);
                }

            }
            
            ///////////////////////////////////////
        }

        public override Vector2Int GetNextStep()
        {
            Vector2Int result = new Vector2Int();
            if (outOfReachTarget.Count > 0)
            {
                result = unit.Pos.CalcNextStepTowards(outOfReachTarget[0]);
                return result;
            }
            else return unit.Pos;
            
        }

        protected override List<Vector2Int> SelectTargets()
        {
            ///////////////////////////////////////
            // Homework 1.4 (1st block, 4rd module)
            ///////////////////////////////////////
            float minDist = float.MaxValue;
            List<Vector2Int> result = new List<Vector2Int>();
            Vector2Int DungTarget = new Vector2Int();
            result = GetAllTargets().ToList();
            if (result.Count > 0)
            {
                foreach (var target in result)
                {
                    if (DistanceToOwnBase(target) < minDist)
                    {
                        minDist = DistanceToOwnBase(target);
                        DungTarget = target;
                    }
                }
                if(IsTargetInRange(DungTarget))
                {
                    outOfReachTarget.Clear();
                    result.Clear();
                    result.Add(DungTarget);
                    return result;
                }
                else
                {
                    outOfReachTarget.Add(DungTarget);
                    result.Clear();
                    return result;
                }
            }
            else
            {
                Vector2Int botBase = runtimeModel.RoMap.Bases[0];

                if (IsTargetInRange(botBase))
                {
                    outOfReachTarget.Clear();
                    result.Add(botBase); 
                    return result;

                }
                else
                {
                    outOfReachTarget.Add(botBase);
                    return result;
                }
            }
            
            ///////////////////////////////////////
        }

        public override void Update(float deltaTime, float time)
        {
            if (_overheated)
            {              
                _cooldownTime += Time.deltaTime;
                float t = _cooldownTime / (OverheatCooldown/10);
                _temperature = Mathf.Lerp(OverheatTemperature, 0, t);
                if (t >= 1)
                {
                    _cooldownTime = 0;
                    _overheated = false;
                }
            }
        }

        private int GetTemperature()
        {
            if(_overheated) return (int) OverheatTemperature;
            else return (int)_temperature;
        }

        private void IncreaseTemperature()
        {
            _temperature += 1f;
            if (_temperature >= OverheatTemperature) _overheated = true;
        }
    }
}