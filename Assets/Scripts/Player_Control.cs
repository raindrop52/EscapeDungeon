using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_Control : Unit
    {
        SpriteRenderer _sprite;
        Animator _anim;

        Vector3 vector;
        [Header("ĳ���� �̵� ����")]
        public float _speed;
        public int _walkCount;
        [SerializeField] int _currentWalkCount;
        public bool _canMove = true;
        [Header("ĳ���� �뽬 ����")]
        public int _dashCount;
        [SerializeField] int _currentDashCount;
        public float _dashSpeed;
        public bool _canDash = true;
        [Header("ĳ���� ���� ����")]
        public bool _canAttack = true;

        [Header("��Ÿ")]
        public PlayerLight _light;
        public Transform _playerPosition;        // �÷��̾� ���� ��ġ

        [SerializeField] CHAR_DIR _nowDir;      // �÷��̾� ���� üũ ��

        [SerializeField] float _x, _y;          // ���̽�ƽ �� üũ
        [SerializeField] Joystick _joystick;
        [SerializeField] LayerMask _layerMask;

        IEnumerator _OnMove()
        {
            while (GetAxis())
            {
                vector.Set(_x, _y, transform.position.z);

                if (vector.x != 0)
                    vector.y = 0;

                _anim.SetFloat("DirX", vector.x);
                _anim.SetFloat("DirY", vector.y);
                _anim.SetBool("Walking", true);

                while (_currentWalkCount < _walkCount)
                {
                    if (vector.x != 0)
                    {
                        transform.Translate(vector.x * _speed, 0, 0);
                    }
                    else if (vector.y != 0)
                    {
                        transform.Translate(0, vector.y * _speed, 0);
                    }

                    _currentWalkCount++;
                    yield return new WaitForSeconds(0.01f);
                }

                _currentWalkCount = 0;
            }

            _anim.SetBool("Walking", false);

            _canMove = true;
        }

        IEnumerator _OnDash()
        {
            _anim.SetTrigger("Dash");

            while (_currentDashCount < _dashCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * _speed, 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * _speed, 0);
                }

                _currentDashCount++;
                Debug.Log("�뽬 �� " + _currentDashCount);
                yield return new WaitForSeconds(0.01f);
            }

            _currentDashCount = 0;

            yield return new WaitForSeconds(1.0f);

            _canDash = true;
        }    

        IEnumerator _OnAttack()
        {
            _anim.SetTrigger("Attacking");

            _canAttack = true;

            yield return new WaitForSeconds(0.5f);
        }

        protected override void Start()
        {
            base.Start();

            _sprite = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
            _light = GetComponentInChildren<PlayerLight>();
        }

        protected override void Update()
        {
            base.Update();

            if(_canDash)
            {
                if(GetDashButton())
                {
                    _canDash = false;
                    StartCoroutine(_OnDash());
                }
            }
            
            // �̵� ���� ����
            if (_canMove)
            {
                if (GetAxis())
                {
                    _canMove = false;
                    StartCoroutine(_OnMove());
                }
            }

            if(_canAttack)
            {
                if(GetAttackButton())
                {
                    _canAttack = false;
                    StartCoroutine(_OnAttack());
                }
            }
        }

        bool GetAxis()
        {
            bool result = false;

            if (_joystick != null)
            {
                _x = _joystick.Horizontal;
                _y = _joystick.Vertical;
            }
            else
            {
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");
            }

            if (_x != 0 || _y != 0)
                result = true;

            return result;
        }

        bool GetDashButton()
        {
            bool result = false;

            if (_joystick != null)
            {
                // ��ư Ŭ�� �� �뽬
            }
            else
            {
                // �����̽��� ���� �� �뽬
                if (Input.GetKeyDown(KeyCode.Space) == true)
                    result = true;
            }

            return result;
        }

        float time;
        float eltime = 0.3f;

        bool GetAttackButton()
        {
            bool result = false;

            if (_joystick != null)
            {
                // ��ư Ŭ�� �� ����
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.X) == true)
                {
                    result = true;
                }
            }

            return result;
        }

        public void ChangePlayerPos(Vector2 pos)
        {
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool onMoving = GameManager._inst.Moving;

            // ��Ż �浹ü�� �ε�����, �̵����°� �ƴ� ���
            if (collision.tag == "Portal" && onMoving == false)
            {
                Debug.Log(collision.name + " ��Ż �ε���");

                // ��Ż ��ũ��Ʈ ������
                Portal portalCol = collision.GetComponent<Portal>();
                if (portalCol != null)
                {
                    GameManager._inst.StartMoveObject(portalCol);
                }
            }
        }
    }
}