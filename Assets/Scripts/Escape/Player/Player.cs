using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player : MonoBehaviour
    {
        [Header("ĳ���� ���� üũ")]
        public bool _useTutorial = true;                    // ���� �� Ʃ�丮�� ������ ����� �޽����� üũ ( �⺻�� true )

        [Header("ĳ���� �ǰ� ����")]
        bool _hit = false;
        public bool HitStatus
        { get { return _hit; } }
        float _hitCoolTime = 1.0f;

        [Header("ĳ���� ��ȭ ����")]
        bool _inTalkArea = false;               // ��ȭ Ʈ���� �� ����
        [SerializeField] bool _talk = false;     // ��ȭ ���� ����
        public bool OnTalk
        { get { return _talk; } set { _talk = value; } }

        [Header("�ߵ� ����")]
        //�ߵ� ���� ����
        Dictionary<Poison_Type, bool> _isPoison;
        PoisonFx[] _poisonFxs;

        [Header("���̽�ƽ ��ư ����")]
        public bool _btnDo = false;

        AudioSource _footstep;

        Player_StateManager _stateMgr;

        Vector3 _playerScale;

        public void Init()
        {
            _playerScale = transform.localScale;

            _isPoison = new Dictionary<Poison_Type, bool>();

            for (int i = 1; i < (int)Poison_Type.Count; i++)
            {
                _isPoison.Add((Poison_Type)i, false);
            }

            _poisonFxs = GetComponentsInChildren<PoisonFx>();
            _footstep = GetComponentInChildren<AudioSource>();

            GameObject stateMgrObj = new GameObject("PlayerStateManager");
            stateMgrObj.transform.parent = transform;
            _stateMgr = stateMgrObj.AddComponent<Player_StateManager>();
            _stateMgr.Init(this);
        }

        public void OnFootStep()
        {
            if (_footstep != null)
                _footstep.Play();
        }

        public void StopFootStep()
        {
            if (_footstep != null)
                _footstep.Stop();
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

        public void ClearPoisonFx()
        {
            if(_poisonFxs.Length > 0)
            {
                foreach(PoisonFx fx in _poisonFxs)
                {
                    fx.Init();
                    fx.StopParticle();
                }
            }                
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

            if (collision.tag == "Death")
            {
                if(GameManager._inst.Die == false)
                {
                    GameManager._inst.Die = true;
                }
            }

            if (collision.tag == "Talk")
            {
                if(!_inTalkArea)
                {
                    _inTalkArea = true;
                    // ��ȭ ������Ʈ�� ����
                    _stateMgr.ChangeState(Player_State.TALK);

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
                    // �Ϲ� ������Ʈ�� ����
                    _stateMgr.ChangeState(Player_State.NORMAL);

                    _inTalkArea = false;
                }
            }
        }

        IEnumerator _HitEffect()
        {
            Animator anim = GetComponent<Animator>();

            anim.SetTrigger("Hit");
            UIManager._inst.RefreshHitUI();
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
            if (UIManager._inst.CheckTalk() == true)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) == true || _btnDo == true)
            {
                _talk = true;

                if (_btnDo == true)
                    _btnDo = false;
            }
        }

        // ĳ���� �̵� �� ��ġ ����
        public void ChangePlayerPos(Vector3 pos)
        {
            transform.position = pos;
            GameManager._inst.SetSpawnPos(pos);
        }

        public void CheckScale()
        {
            if (_playerScale != transform.localScale)
            {
                transform.localScale = _playerScale;
            }
        }
    }
}