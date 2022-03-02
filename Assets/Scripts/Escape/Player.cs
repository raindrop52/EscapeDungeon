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
        bool _inTalkArea = false;               // ��ȭ Ʈ���� �� ����
        [SerializeField] bool _talk = false;     // ��ȭ ���� ����
        public bool OnTalk
        { get { return _talk; } set { _talk = value; } }


        [Header("�ߵ� ����")]
        //�ߵ� ���� ����
        Dictionary<Poison_Type, bool> _isPoison;
        
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

            anim.SetTrigger("Hit");
            UIManager._inst._statusUI.OnHitUI();
            yield return new WaitForSeconds(_hitCoolTime);

            _hit = false;
        }

        IEnumerator _TalkCheck(GameObject col)
        {
            Trap_Talk trigger = col.GetComponent<Trap_Talk>();
            if (trigger != null)
            {
                while (_inTalkArea)
                {
                    IsTalk();

                    // ��ȭ�ϱⰡ ���� ���
                    if (_talk == true)
                    {
                        trigger.ExecuteTriggerEvent();

                        _talk = false;
                    }

                    yield return null;
                }

                if(_talk == true)
                {
                    _talk = false;
                }

                // ��ȭâ ���� �ݱ�
                UIManager._inst.ShowTextMessage("", true);
            }
        }

        void IsTalk()
        {
            if (UIManager._inst.Talking == true)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                _talk = true;
            }
        }

        // ĳ���� �̵� �� ��ġ ����
        public void ChangePlayerPos(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}