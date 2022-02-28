using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Paralysis : Poison
    {
        Player_Control _pc = null;

        protected override void ExecutePoison(Player player)
        {
            if (_pc == null)
                _pc = player.GetComponent<Player_Control>();

            if (_pc != null)
            {
                // 플레이어 이동 제한
                _pc.enabled = false;
            }

            base.ExecutePoison(player);
        }

        protected override void ClosePoison()
        {
            base.ClosePoison();

            if (_pc != null)
            {
                // 플레이어 이동 제한 해제
                _pc.enabled = true;
            }
        }
    }
}