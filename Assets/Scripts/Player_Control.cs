using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_Control : Unit
    {
        // 충돌 검사
        BoxCollider2D _boxCol;
        public LayerMask _layerMask;    // 통과 불가 오브젝트 설정
        SpriteRenderer _sprite;
        Animator _anim;

        Vector3 vector;
        [Header("캐릭터 이동 관련")]
        public float _speed;
        public int _walkCount;
        [SerializeField] int _currentWalkCount;
        public bool _canMove = true;
        /*
        [Header("캐릭터 대쉬 관련")]
        public int _dashCount;
        [SerializeField] int _currentDashCount;
        public bool _canDash = true;
        [Header("캐릭터 공격 관련")]
        public bool _canAttack = true;
        */
        [Header("기타")]
        public PlayerLight _light;
        public Transform _playerPosition;        // 플레이어 시작 위치

        [Header("조이스틱")]
        [SerializeField] float _x, _y;          // 조이스틱 값 체크
        [SerializeField] Joystick _joystick;
        /*
        bool _joystickDashOn = false;           // 조이스틱 대쉬 체크
        bool _joystickAttackOn = false;           // 조이스틱 어택 체크
        */
        Player _player;

        IEnumerator _OnMove()
        {
            while (GetAxis())
            {
                if (GameManager._inst.MapMoving == true)
                {
                    break;
                }

                vector.Set(_x, _y, transform.position.z);

                if (vector.x != 0)
                    vector.y = 0;

                _anim.SetFloat("DirX", vector.x);
                _anim.SetFloat("DirY", vector.y);

                if (vector.x < 0)
                    _sprite.flipX = true;
                else
                    _sprite.flipX = false;
                                
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

        /*
        // 애니메이션 스크립트
        IEnumerator _OnDash()
        {
            // 대쉬 시작 전 무적 설정
            _player._dashDodge = true;

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

            // 이동이 완료된 후 무적 회피 해제
            _player._dashDodge = false;
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
        */

        protected override void Start()
        {
            base.Start();

            _sprite = GetComponent<SpriteRenderer>();
            _boxCol = GetComponent<BoxCollider2D>();
            _anim = GetComponent<Animator>();
            _light = GetComponentInChildren<PlayerLight>();
            _player = GetComponent<Player>();
        }

        protected override void Update()
        {
            base.Update();

            if (GameManager._inst.MapMoving == false)
            {
                // 이동 가능 상태
                if (_canMove)
                {
                    if (GetAxis())
                    {
                        _canMove = false;
                        StartCoroutine(_OnMove());
                    }
                }

                // 대쉬 상태
                //if (_canDash)
                //{
                //    if (GetDashButton())
                //    {
                //        _canDash = false;
                //        _anim.SetTrigger("Dash");
                //    }
                //}

                // 공격상태
                //if (_canAttack)
                //{
                //    if (GetAttackButton())
                //    {
                //        _canAttack = false;
                //        StartCoroutine(_OnAttack());
                //    }
                //}
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
                
            }
            else
            {
                // 스페이스바 눌릴 시 대쉬
                if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    result = true;
                }
            }

            return result;
        }

        bool GetAttackButton()
        {
            bool result = false;

            if (_joystick != null)
            {
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

        /*
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
        */
    }
}