using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EscapeGame
{
    public enum Talk_ID
    {
        NONE = 0,           // 상황 발생 X
        READ,               // 읽는 상황
        BREAK,              // 파괴 상황
        MOVE,               // 이동 상황

    }

    [Serializable]
    public class TalkInfo
    {
        public Talk_ID _id;     // 상황 ID
        public int _level;      // 스테이지 레벨
        public int _order;      // 동작 순서
    }

    public class TalkTrigger : MonoBehaviour
    {
        [SerializeField] protected TalkInfo _talkInfo;

        void Start()
        {

        }

        
        void Update()
        {

        }

        protected virtual void DoTrigerEvent()
        {

        }

        public void ExcuteTriggerEvent()
        {
            if(_talkInfo != null)
            {
                switch(_talkInfo._id)
                {
                    case Talk_ID.READ:
                        {
                            break;
                        }
                        // 파괴 상황
                    case Talk_ID.BREAK:
                        {
                            // 스테이지 레벨 체크
                            if(_talkInfo._level == GameManager._inst._stageLevel)
                            {
                                DoTrigerEvent();
                            }

                            break;
                        }
                    case Talk_ID.MOVE:
                        {
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}