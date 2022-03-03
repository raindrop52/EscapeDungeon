using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Player_Status
    {
        IDLE = 0,
        MOVE,
        TALK,
        HIT,
        NOTICE,
    }

    public class Player : MonoBehaviour
    {
        [Header("캐릭터 상태 체크")]
        public Player_Status _status = Player_Status.IDLE;
        public bool _useTutorial = true;                    // 시작 전 튜토리얼 진행을 물어보는 메시지로 체크 ( 기본은 true )

        [Header("캐릭터 피격 관련")]
        bool _hit = false;
        public bool HitStatus
        { get { return _hit; } }
        float _hitCoolTime = 1.0f;

        [Header("캐릭터 대화 관련")]
        bool _inTalkArea = false;               // 대화 트리거 존 입장
        [SerializeField] bool _talk = false;     // 대화 상태 진입
        public bool OnTalk
        { get { return _talk; } set { _talk = value; } }

        [Header("중독 관련")]
        //중독 상태 여부
        Dictionary<Poison_Type, bool> _isPoison;

        [Header("조이스틱 버튼 관련")]
        public bool _btnDo = false;
        
        public void Init()
        {
            _isPoison = new Dictionary<Poison_Type, bool>();

            for (int i = 1; i < (int)Poison_Type.Count; i++)
            {
                _isPoison.Add((Poison_Type)i, false);
            }
        }

        public bool GetPoisonStatus(Poison_Type key)
        {
            bool value;

            value = _isPoison[key];

            return value;
        }

        public void SetPoisonStatus(Poison_Type key, bool status)
        {
            _isPoison[key] = status;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Attack")
            {
                if (!_hit)
                {
                    _hit = true;
                    _status = Player_Status.HIT;
                    StartCoroutine(_HitEffect());
                }
            }

            if (collision.tag == "Talk")
            {
                if(!_inTalkArea)
                {
                    _inTalkArea = true;
                    StartCoroutine(_TalkCheck(collision.gameObject));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Talk")
            {
                if (_inTalkArea)
                {
                    _inTalkArea = false;
                }
            }
        }

        IEnumerator _HitEffect()
        {
            Animator anim = GetComponent<Animator>();

            anim.SetTrigger("Hit");
            UIManager._inst._statusUI.OnHitUI();
            yield return new WaitForSeconds(_hitCoolTime);

            _hit = false;
            _status = Player_Status.IDLE;
        }

        IEnumerator _TalkCheck(GameObject col)
        {
            Trap_Talk trigger = col.GetComponent<Trap_Talk>();
            if (trigger != null)
            {
                while (_inTalkArea)
                {
                    IsTalk();

                    // 대화하기가 눌린 경우
                    if (_talk == true)
                    {
                        trigger.ExecuteTriggerEvent();

                        _talk = false;
                        _status = Player_Status.IDLE;
                    }

                    yield return null;
                }

                if(_talk == true)
                {
                    _talk = false;
                    _status = Player_Status.IDLE;
                }

                // 대화창 강제 닫기
                UIManager._inst.ShowTextMessage("", true);
            }
        }

        void IsTalk()
        {
            if (UIManager._inst.Talking == true)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) == true || _btnDo == true)
            {
                _talk = true;
                _status = Player_Status.TALK;

                if (_btnDo == true)
                    _btnDo = false;
            }
        }

        // 캐릭터 이동 시 위치 변경
        public void ChangePlayerPos(Vector3 pos)
        {
            transform.position = pos;
        }

    }
}