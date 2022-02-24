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
        protected ParticleSystem _effectNotice;

        protected virtual void Start()
        {
            Transform noticeTrans = transform.Find("Notice");
            if(noticeTrans != null)
                _effectNotice = noticeTrans.GetComponent<ParticleSystem>();
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            base.OnDrawGizmos();
        }

        protected virtual void DoTrigerEvent()
        {
            // 트리거 동작 시 이펙트 숨김 (영구히)
            ShowEffect(false);
        }

        public void ShowEffect(bool show)
        {
            if (_effectNotice != null)
                _effectNotice.gameObject.SetActive(show);
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

                // Panel이 없어질때까지 대기
                yield return new WaitUntil(() => UIManager._inst.IsTextPanel() == false);

                DoTrigerEvent();
            }
            yield return null;
        }

        public void ExecuteTriggerEvent()
        {
            if (_talkInfo != null)
            {
                StartCoroutine(_OnTrigger());
            }
        }
    }
}