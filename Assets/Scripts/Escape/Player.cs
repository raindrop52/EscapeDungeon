using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player : MonoBehaviour
    {
        [Header("캐릭터 피격 관련")]
        bool _hit = false;
        public bool HitStatus
        {
            get
            {
                return _hit;
            }
        }
        float _hitCoolTime = 1.0f;

        [Header("캐릭터 대화 관련")]
        bool _inTalkArea = false;       // 대화 트리거 존 입장
        bool _talk = false;             // 대화 버튼 클릭
        bool _talking = false;          // 대화 동작 중
        public bool OnTalk
        {
            get { return _talk; }
            set { _talk = value; }
        }


        void Start()
        {
        }

        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Attack")
            {
                if (!_hit)
                {
                    _hit = true;
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

            if (collision.tag == "Portal")
            {
                GameManager._inst.StartMoveStage();
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

            if( anim.GetCurrentAnimatorStateInfo(0).IsName("Dash Tree") )
            {
                yield return null;
            }
            else
            {
                anim.SetTrigger("Hit");

                yield return new WaitForSeconds(_hitCoolTime);
            }

            _hit = false;
        }

        IEnumerator _TalkCheck(GameObject col)
        {
            Trap_Talk trigger = col.GetComponent<Trap_Talk>();
            if (trigger != null)
            {
                while (_inTalkArea)
                {
                    _talk = TalkCheck();
                    // 대화하기가 눌린 경우
                    if (_talk)
                    {
                        _talking = true;

                        trigger.ExcuteTriggerEvent();

                        _talking = false;
                    }

                    yield return null;
                }

                // 대화창 강제 닫기
                UIManager._inst.ShowTextMessage("", true);
            }
        }

        bool TalkCheck()
        {
            if(_talking == true)
            {
                return false;
            }

            // Space 키 눌리면 _talk = true;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }

            return false;
        }

        // 캐릭터 이동 시 위치 변경
        public void ChangePlayerPos(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}