using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_Control : Unit
    {
        // �浹 �˻�
        BoxCollider2D _boxCol;
        public LayerMask _layerMask;    // ��� �Ұ� ������Ʈ ����
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
        public bool _canDash = true;
        [Header("ĳ���� ���� ����")]
        public bool _canAttack = true;
        [Header("ĳ���� �ǰ� ����")]
        bool _hit = false;
        float _hitCoolTime = 1.0f;

        [Header("��Ÿ")]
        public PlayerLight _light;
        public Transform _playerPosition;        // �÷��̾� ���� ��ġ

        [Header("���̽�ƽ")]
        [SerializeField] float _x, _y;          // ���̽�ƽ �� üũ
        [SerializeField] Joystick _joystick;
        bool _joystickDashOn = false;           // ���̽�ƽ �뽬 üũ
        bool _joystickAttackOn = false;           // ���̽�ƽ ���� üũ

        IEnumerator _OnMove()
        {
            while (GetAxis())
            {
                vector.Set(_x, _y, transform.position.z);

                if (vector.x != 0)
                    vector.y = 0;

                _anim.SetFloat("DirX", vector.x);
                _anim.SetFloat("DirY", vector.y);

                if (vector.x < 0)
                    _sprite.flipX = true;
                else
                    _sprite.flipX = false;

                RaycastHit2D hit;
                Vector2 start = transform.position;  // ĳ������ ��ġ
                Vector2 end = start + new Vector2(vector.x * _speed * _walkCount, vector.y * _speed * _walkCount);    // ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ

                _boxCol.enabled = false;
                hit = Physics2D.Linecast(start, end, _layerMask);
                _boxCol.enabled = true;

                if(hit.transform != null)
                {
                    break;
                }

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

        // �ִϸ��̼� ��ũ��Ʈ
        IEnumerator _OnDash()
        {
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
                yield return new WaitForSeconds(0.01f);
            }

            _currentDashCount = 0;

            yield return new WaitForSeconds(1.0f);

            if(_joystick != null)
            {
                if (_joystickDashOn == true)
                    _joystickDashOn = false;
            }    
            _canDash = true;
        }    

        IEnumerator _OnAttack()
        {
            _anim.SetTrigger("Attacking");

            if (_joystickAttackOn)
                _joystickAttackOn = false;

            _canAttack = true;

            yield return new WaitForSeconds(0.5f);
        }

        protected override void Start()
        {
            base.Start();

            _sprite = GetComponent<SpriteRenderer>();
            _boxCol = GetComponent<BoxCollider2D>();
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
                    _anim.SetTrigger("Dash");
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
                result = _joystickDashOn;
            }
            else
            {
                // �����̽��� ���� �� �뽬
                if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    result = true;
                }
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
                result = _joystickAttackOn;
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

        public void OnDashButton()
        {
            if (!_joystickDashOn)
                _joystickDashOn = true;
        }

        public void OnAttackButton()
        {
            if (!_joystickAttackOn)
                _joystickAttackOn = true;
        }

        // ĳ���� �̵� �� ��ġ ����
        public void ChangePlayerPos(Vector2 pos)
        {
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Attack")
            {
                if(!_hit)
                {
                    _hit = true;
                    StartCoroutine(_HitEffect());
                }
            }
        }

        IEnumerator _HitEffect()
        {
            _anim.SetTrigger("Hit");

            yield return new WaitForSeconds(_hitCoolTime);

            _hit = false;
        }
    }
}