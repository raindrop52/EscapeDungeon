using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_Control : Unit
    {
        BoxCollider2D _boxCol;          // 충돌 검사
        public LayerMask _layerMask;    // 통과 불가 오브젝트 설정
        SpriteRenderer _sprite;
        Animator _anim;

        Vector3 vector;

        [Header("캐릭터 이동 관련")]
        public float _speed;
        public int _walkCount;
        [SerializeField] int _currentWalkCount;
        public bool _canMove = true;

        [Header("기타")]
        public PlayerLight _light;
        public Transform _playerPosition;        // 플레이어 시작 위치

        [Header("조이스틱")]
        [SerializeField] float _x, _y;          // 조이스틱 값 체크
        [SerializeField] Joystick _joystick;
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

        public void OnTalkButton()
        {
            _player.OnTalk = true;
        }
    }
}