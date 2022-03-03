using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Trap_Talk : Trap
    {
        [Serializable]
        public class TalkInfo
        {
            public Talk_ID _id;             // ��Ȳ ID
            public int _level;              // �������� ����
            public int _order;              // ���� ����
            public string _text = "";       // ���� �޽���
        }

        [SerializeField] protected TalkInfo _talkInfo;

        protected virtual void Start()
        {
            
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            base.OnDrawGizmos();
        }

        protected virtual void DoTrigerEvent()
        {
            
        }

        bool IsShowText()
        {
            // �ؽ�Ʈ �޽����� Ȱ��ȭ ���� üũ
            bool result = false;
            result = UIManager._inst.IsTextPanel();
            return result;
        }

        void OnShowText()
        {
            UIManager._inst.ShowTextMessage(_talkInfo._text);
        }

        IEnumerator _OnTrigger()
        {
            if (_talkInfo != null)
            {
                // Text �޽��� â�� ����������
                if(IsShowText() == false)
                {
                    // Text �޽��� â�� ����
                    OnShowText();

                    // Text �޽��� â�� ���������� ���
                    yield return new WaitUntil(() => IsShowText() == false);

                    DoTrigerEvent();
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        public void ExecuteTriggerEvent()
        {
            if (_talkInfo != null && UIManager._inst.Talking == false)
            {
                StartCoroutine(_OnTrigger());
            }
        }
    }
}