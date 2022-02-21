using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player : MonoBehaviour
    {
        [Header("ĳ���� �ǰ� ����")]
        bool _hit = false;
        public bool HitStatus
        {
            get
            {
                return _hit;
            }
        }
        float _hitCoolTime = 1.0f;

        [Header("ĳ���� ��ȭ ����")]
        bool _inTalkArea = false;       // ��ȭ Ʈ���� �� ����
        bool _talk = false;             // ��ȭ ��ư Ŭ��
        bool _talking = false;          // ��ȭ ���� ��
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
                    // ��ȭ�ϱⰡ ���� ���
                    if (_talk)
                    {
                        _talking = true;

                        trigger.ExcuteTriggerEvent();

                        _talking = false;
                    }

                    yield return null;
                }

                // ��ȭâ ���� �ݱ�
                UIManager._inst.ShowTextMessage("", true);
            }
        }

        bool TalkCheck()
        {
            if(_talking == true)
            {
                return false;
            }

            // Space Ű ������ _talk = true;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }

            return false;
        }

        // ĳ���� �̵� �� ��ġ ����
        public void ChangePlayerPos(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}