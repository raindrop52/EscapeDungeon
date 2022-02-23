using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Slow : Poison
    {
        [SerializeField] float _slowSpeedPer = 2.0f;
        Player_Control _pc = null;

        protected override void ExecutePoison(Player player)
        {
            if(_pc == null)
                _pc = player.GetComponent<Player_Control>();

            if (_pc != null)
            {
                // 슬로우에 걸리지 않은 상태
                if(_pc.SlowSpeed == 0.0f)
                {
                    _pc.SlowSpeed = _pc.Speed / _slowSpeedPer;
                }
            }

            // 중독 시작
            base.ExecutePoison(player);
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
        }
    }
}