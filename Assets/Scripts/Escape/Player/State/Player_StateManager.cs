using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Player_State
    {
        INVALID = -1,
        NORMAL = 0,
        TALK,
        POISON,
        
        END
    }

    public class Player_StateManager : MonoBehaviour
    {
        public static Player_StateManager _inst;

        Player _owner;
        public Player Owner
        { get { return _owner; } }
        List<Player_StateBase> _states;
        Player_State _curState = Player_State.INVALID;         // �÷��̾��� ���� ����
        public Player_State CurState
        { get { return _curState; } }

        private void Awake()
        {
            _inst = this;
        }

        public void Init(Player owner)
        {
            _states = new List<Player_StateBase>();
            _owner = owner;
            Player_StateBase stateObj = null;

            // �ʱ�ȭ �� enum�� ���ǵ� State ��ü�� �����Ͽ� ĳ���Ϳ� ��ġ
            for (int i = (int)Player_State.INVALID + 1; i < (int)Player_State.END; i++)
            {
                Player_State state = (Player_State)i;
                switch (state)
                {
                    case Player_State.NORMAL:
                        {
                            stateObj = gameObject.AddComponent<Player_State_Normal>();
                            break;
                        }
                    case Player_State.POISON:
                        {
                            stateObj = gameObject.AddComponent<Player_State_Poison>();
                            break;
                        }
                    case Player_State.TALK:
                        {
                            stateObj = gameObject.AddComponent<Player_State_Talk>();
                            break;
                        }
                }

                stateObj.enabled = false;
                stateObj.Init(this);

                _states.Add(stateObj);
            }

            ChangeState(Player_State.NORMAL);
        }

        public void ChangeState(Player_State newState)
        {
            Player_State prevState = _curState;

            // ������ ������Ʈ ��ü�� �� ���� ������Ʈ���Դ� exit ���� �˸���, �� ������Ʈ���Դ� enter�� �˸�
            if (prevState != Player_State.INVALID)
            {
                _states[(int)prevState].OnExit();
            }

            _states[(int)newState].OnEnter();

            _curState = newState;
        }
    }
}