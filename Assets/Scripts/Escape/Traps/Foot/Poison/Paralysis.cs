using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Paralysis : Poison
    {
        Player_Control _pc = null;

        protected override void ExecutePoison()
        {
            if (_pc == null)
                _pc = _target.GetComponent<Player_Control>();

            if (_pc != null)
            {
                // �÷��̾� �̵� ����
                _pc.enabled = false;
            }

            // ����Ʈ ����
            if (_fx != null)
            {
                _fx.Init();
                _fx.PlayParticle();
            }

            base.ExecutePoison();
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