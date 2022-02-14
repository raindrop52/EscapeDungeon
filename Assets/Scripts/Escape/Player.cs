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
        bool _talk = false;
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
                if(!_talk)
                {
                    StartCoroutine(_TalkCheck(collision.gameObject));
                }
            }

            if (collision.tag == "Portal")
            {
                GameManager._inst.StartMoveStage();
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
            while(true)
            {
                _talk = TalkCheck();
                // ��ȭ�ϱⰡ ���� ���
                if (_talk)
                {
                    TalkTrigger trigger = col.GetComponent<TalkTrigger>();
                    if (trigger != null)
                    {
                        trigger.ExcuteTriggerEvent();
                        break;
                    }
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        bool TalkCheck()
        {
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