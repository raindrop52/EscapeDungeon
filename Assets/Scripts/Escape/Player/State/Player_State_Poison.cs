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
            // �ߵ� ��� ����
            EnterPoison();

            float time = 0.0f;

            while(_poisoning)
            {
                if (time >= _time)
                    break;

                time += Time.fixedDeltaTime;

                yield return null;
            }

            // �ߵ� ��� ����
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
        // �ߵ� ����Ʈ ����Ʈ
        PoisonFx[] _poisonFxList;
        // �ߵ� ���� ���� ����Ʈ
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
                // �̹� �ߵ��� �ɸ� ������ ���
                if(poison._type == type)
                {

                }
                // �ߵ� ���°� ó���� ���
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
                // �ߵ� ������ 0������ ��� �ߵ� ���� ����
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