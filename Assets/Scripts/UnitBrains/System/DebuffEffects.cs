using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UnitBrains.System
{
    public enum DebuffType
    {
        SlowAttack,
        SlowMovement,
        None
    }
    public class DebuffEffects : IStatusEffect
    {
        private string _name;
        private float _duration = 15f;
        private float _attackSpeedModifier = 1;
        private float _moveModifier = 1;

        public string Name => _name;
        public float Duration => _duration;
        public float AttackSpeedModifier => _attackSpeedModifier;
        public float MoveModifier => _moveModifier;

        public DebuffEffects()
        {
            int rand = UnityEngine.Random.Range(0, 3);
            switch (rand)
            {

                case 0:
                    _name = DebuffType.SlowAttack.ToString();
                    _attackSpeedModifier = 2f;
                    break;
                case 1:
                    _name = DebuffType.SlowMovement.ToString();
                    _moveModifier = 10f;
                    break;
                case 2:
                    _name = DebuffType.None.ToString();
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

