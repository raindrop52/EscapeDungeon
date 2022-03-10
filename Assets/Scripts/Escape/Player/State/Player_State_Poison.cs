using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_Poison_Type
    {
        public Poison_Type _type = Poison_Type.None;
        public bool _poisoning = false;
        public float _time = 0.0f;

        public Player_Poison_Type(Poison_Type type, float time)
        {
            _type = type;
            _time = time;
            
        }

        IEnumerator _Poisoning()
        {
            _poisoning = true;
            // 중독 기능 실행
            EnterPoison();

            float time = 0.0f;

            while(_poisoning)
            {
                if (time >= _time)
                    break;

                time += Time.fixedDeltaTime;

                yield return null;
            }

            // 중독 기능 해제
            ExitPoison();
            _poisoning = false;
        }

        void EnterPoison()
        {

        }

        void ExitPoison()
        {

        }
    }

    public class Player_State_Poison : Player_StateBase
    {
        // 중독 이펙트 리스트
        PoisonFx[] _poisonFxList;
        // 중독 상태 관련 리스트
        List<Player_Poison_Type> _poisonList = new List<Player_Poison_Type>();

        public override void Init(Player_StateManager stateMgr)
        {
            base.Init(stateMgr);

            Player player = stateMgr.Owner;
            if (player != null)
            {
                Transform poisonTrans = player.transform.Find("PoisonFx");
                if (poisonTrans != null)
                {
                    _poisonFxList = poisonTrans.GetComponentsInChildren<PoisonFx>();
                }
            }
        }

        public void OnPoison(Poison_Type type, float time)
        {
            foreach(Player_Poison_Type poison in _poisonList)
            {
                // 이미 중독이 걸린 상태인 경우
                if(poison._type == type)
                {

                }
                // 중독 상태가 처음인 경우
                else
                {
                    Player_Poison_Type po = new Player_Poison_Type(type, time);
                    _poisonList.Add(po);
                }
            }
        }

        private void FixedUpdate()
        {
            if(_poisonList.Count <= 0)
            {
                // 중독 갯수가 0이하인 경우 중독 상태 해제
                Player_StateManager._inst.ChangeState(Player_State.NORMAL);
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