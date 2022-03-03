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
            public Talk_ID _id;             // 상황 ID
            public int _level;              // 스테이지 레벨
            public int _order;              // 동작 순서
            public string _text = "";       // 읽을 메시지
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
            // 텍스트 메시지의 활성화 여부 체크
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
                // Text 메시지 창이 꺼져있으면
                if(IsShowText() == false)
                {
                    // Text 메시지 창을 켜줌
                    OnShowText();

                    // Text 메시지 창이 꺼질때까지 대기
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