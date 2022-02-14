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
        public Talk_ID _id;             // ��Ȳ ID
        public int _level;              // �������� ����
        public int _order;              // ���� ����
        public string _text = "";       // ���� �޽���
    }

    public class TalkTrigger : MonoBehaviour
    {
        [SerializeField] protected TalkInfo _talkInfo;
        protected ParticleSystem _effect;

        void Start()
        {
            _effect = GetComponentInChildren<ParticleSystem>();
            if (_effect != null)
                ShowEffect(false);
        }

        protected virtual void DoTrigerEvent()
        {
            // Ʈ���� ���� �� ����Ʈ ���� (������)
            ShowEffect(false);
        }

        public void ShowEffect(bool show)
        {
            _effect.gameObject.SetActive(show);
        }

        public void ExcuteTriggerEvent()
        {
            if(_talkInfo != null)
            {
                switch(_talkInfo._id)
                {
                    // ��ȭ ��Ȳ
                    case Talk_ID.READ:
                        {
                            DoTrigerEvent();

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