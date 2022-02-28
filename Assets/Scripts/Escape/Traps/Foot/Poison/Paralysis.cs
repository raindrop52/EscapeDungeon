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
                // �÷��̾� �̵� ����
                _pc.enabled = false;
            }

            base.ExecutePoison(player);
        }

        protected override void ClosePoison()
        {
            base.ClosePoison();

            if (_pc != null)
            {
                // �÷��̾� �̵� ���� ����
                _pc.enabled = true;
            }
        }
    }
}