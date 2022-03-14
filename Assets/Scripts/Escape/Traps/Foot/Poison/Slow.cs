using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Slow : Poison
    {
        [SerializeField] float _slowSpeedPer = 2.0f;
        Player_Control _pc = null;

        public override void OnPoison(Player player, Callback cb)
        {
            if(_fx == null)
            {
                _fx = player.transform.Find("SlowFX").GetComponent<PoisonFx>();
            }

            base.OnPoison(player, cb);
        }

        protected override void ExecutePoison()
        {
            if(_pc == null)
                _pc = _target.GetComponent<Player_Control>();

            if (_pc != null)
            {
                // ���ο쿡 �ɸ��� ���� ����
                if(_pc.SlowSpeed == 0.0f)
                {
                    _pc.SlowSpeed = _pc.Speed / _slowSpeedPer;
                }
            }

            // ����Ʈ ����
            if (_fx != null)
            {
                _fx.transform.position = _target.transform.position;
                _fx.Init();
                _fx.PlayParticle();
            }

            // �ߵ� ����
            base.ExecutePoison();
        }

        protected override void ClosePoison()
        {
            base.ClosePoison();

            if(_pc != null)
            {
                if (_pc.SlowSpeed != 0.0f)
                {
                    _pc.SlowSpeed = 0.0f;
                }
            }

            if(_fx != null)
            {
                _fx.StopParticle();
            }
        }
    }
}