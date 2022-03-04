using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_State_Talk : Player_StateBase
    {
        // �÷��̾ ������ �ִ� �˸� ����Ʈ
        ParticleSystem _psNotice;

        public override void Init(Player_StateManager stateMgr)
        {
            base.Init(stateMgr);

            if(_psNotice == null)
            {
                Player player = stateMgr.Owner;
                if(player != null)
                {
                    _psNotice = player.transform.Find("TalkFX").GetComponent<ParticleSystem>();
                }
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _psNotice.Play();
        }

        public override void OnExit()
        {

            base.OnExit();
        }
    }
}