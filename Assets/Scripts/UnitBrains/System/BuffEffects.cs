using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts.UnitBrains.System
{
    public enum BuffType
    {
        FastAttack,
        FastMovement,
        None
    }
    public class BuffEffects : IStatusEffect
    {
        private string _name;
        private float _duration = 3f;
        private float _attackSpeedModifier = 1;
        private float _moveModifier = 1;

        public string Name => _name;
        public float Duration => _duration;
        public float AttackSpeedModifier => _attackSpeedModifier;
        public float MoveModifier => _moveModifier;

        public BuffEffects()
        {
            int rand = UnityEngine.Random.Range(0, 3);
            switch (rand)
            {

                case 0:
                    _name = BuffType.FastAttack.ToString();
                    _attackSpeedModifier = 0.00001f;
                    break;
                case 1:
                    _name = BuffType.FastMovement.ToString();
                    _moveModifier = 0.5f;
                    break;
                case 2:
                    _name = BuffType.None.ToString();
                    _duration = float.MaxValue;
                    break;
            }
        }
        public BuffEffects(BuffType type) 
        {
            switch (type)
            {

                case BuffType.FastAttack:
                    _name = BuffType.FastAttack.ToString();
                    _attackSpeedModifier = 0.0001f;
                    break;
                case BuffType.FastMovement:
                    _name = BuffType.FastMovement.ToString();
                    _moveModifier = 0.5f;
                    break;
                case BuffType.None:
                    _name = BuffType.None.ToString();
                    _duration = float.MaxValue;
                    break;
            }
        }
        public void BuffBreak()
        {
            _name = BuffType.None.ToString();
            _attackSpeedModifier = 1f;
            _moveModifier = 1f;
        }
    }
}
