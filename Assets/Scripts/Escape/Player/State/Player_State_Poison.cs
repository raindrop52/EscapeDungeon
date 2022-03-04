using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_State_Poison : Player_StateBase
    {
        // 중독 이펙트 리스트
        PoisonFx[] _poisonList;

        public override void Init(Player_StateManager stateMgr)
        {
            base.Init(stateMgr);

            Player player = stateMgr.Owner;
            if (player != null)
            {
                Transform poisonTrans = player.transform.Find("PoisonFx");
                if (poisonTrans != null)
                {
                    _poisonList = poisonTrans.GetComponentsInChildren<PoisonFx>();
                }
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();

        }

        public override void OnExit()
        {

            base.OnExit();
        }
    }
}