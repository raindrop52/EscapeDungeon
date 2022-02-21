using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Trap_Talk : Trap
    {
        public enum Talk_ID
        {
            NONE = 0,           // ��Ȳ �߻� X
            BREAK,              // �ı� ��Ȳ
            MOVE,               // �̵� ��Ȳ
            TRAP,               // ���� �ߵ� ��Ȳ
        }

        [Serializable]
        public class TalkInfo
        {
            public Talk_ID _id;             // ��Ȳ ID
            public int _level;              // �������� ����
            public int _order;              // ���� ����
            public string _text = "";       // ���� �޽���
        }

        [SerializeField] protected TalkInfo _talkInfo;
        protected ParticleSystem _effect;

        void Start()
        {
            _effect = GetComponentInChildren<ParticleSystem>();
        }

        protected virtual void DoTrigerEvent()
        {
            // Ʈ���� ���� �� ����Ʈ ���� (������)
            ShowEffect(false);
        }

        public void ShowEffect(bool show)
        {
            if (_effect != null)
                _effect.gameObject.SetActive(show);
        }

        void OnShowText()
        {
            UIManager._inst.ShowTextMessage(_talkInfo._text);
        }

        IEnumerator _OnTrigger()
        {
            if (_talkInfo != null)
            {
                OnShowText();

                yield return new WaitForSeconds(0.5f);

                // Panel�� ������������ ���
                yield return new WaitUntil(() => UIManager._inst.IsTextPanel() == false);

                DoTrigerEvent();
            }
            yield return null;
        }

        public void ExcuteTriggerEvent()
        {
            if (_talkInfo != null)
            {
                StartCoroutine(_OnTrigger());
            }
        }
    }
}