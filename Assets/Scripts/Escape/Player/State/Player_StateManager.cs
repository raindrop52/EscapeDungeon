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
        Player_State _curState = Player_State.INVALID;         // 플레이어의 현재 상태
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

            // 초기화 시 enum의 정의된 State 객체를 생성하여 캐릭터에 배치
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

            // 각각의 스테이트 객체들 중 이전 스테이트에게는 exit 임을 알리고, 새 스테이트에게는 enter를 알림
            if (prevState != Player_State.INVALID)
            {
                _states[(int)prevState].OnExit();
            }

            _states[(int)newState].OnEnter();

            _curState = newState;
        }
    }
}