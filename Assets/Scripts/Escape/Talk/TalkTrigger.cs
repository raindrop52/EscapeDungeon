using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EscapeGame
{
    public enum Talk_ID
    {
        NONE = 0,           // ��Ȳ �߻� X
        READ,               // �д� ��Ȳ
        BREAK,              // �ı� ��Ȳ
        MOVE,               // �̵� ��Ȳ

    }

    [Serializable]
    public class TalkInfo
    {
        public Talk_ID _id;     // ��Ȳ ID
        public int _level;      // �������� ����
        public int _order;      // ���� ����
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
                        // �ı� ��Ȳ
                    case Talk_ID.BREAK:
                        {
                            // �������� ���� üũ
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