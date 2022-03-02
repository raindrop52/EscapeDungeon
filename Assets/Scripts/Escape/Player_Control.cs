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
        Player _player;

        Vector3 vector;

        [Header("캐릭터 이동 관련")]
        [SerializeField] float _speed;
        public float Speed
        { get { return _speed; } }
        public int _walkCount;
        [SerializeField] int _currentWalkCount;
        public bool _canMove = true;

        [Header("기타")]
        public PlayerLight _light;
        public Transform _playerPosition;        // 플레이어 시작 위치

        [Header("조이스틱")]
        [SerializeField] float _x, _y;          // 조이스틱 값 체크
        [SerializeField] Joystick _joystick;
        [SerializeField] GameObject _talkButtonGo;

        [Header("중독 상태")]
        float _slowSpeed;                     // Slow 이동속도
        public float SlowSpeed
        { get { return _slowSpeed; } set { _slowSpeed = value; } }

        IEnumerator _OnMove()
        {
            while (GetAxis())
            {
                if (GameManager._inst.MapMoving == true)
                {
                    break;
                }

                if(this.enabled == false)
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

                float speed = _speed;
                // 중독:슬로우에 걸린 경우
                if(_slowSpeed > 0.0f)
                {
                    speed = _slowSpeed;
                }

                while (_currentWalkCount < _walkCount)
                {
                    if (vector.x != 0)
                    {
                        transform.Translate(vector.x * speed, 0, 0);
                    }
                    else if (vector.y != 0)
                    {
                        transform.Translate(0, vector.y * speed, 0);
                    }

                    _currentWalkCount++;
                    yield return new WaitForSeconds(0.01f);
                }

                _currentWalkCount = 0;
            }

            _anim.SetBool("Walking", false);

            _canMove = true;
        }

        void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _boxCol = GetComponent<BoxCollider2D>();
            _anim = GetComponent<Animator>();
            _light = GetComponentInChildren<PlayerLight>();
            _player = GetComponent<Player>();
        }

        void FixedUpdate()
        {
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

        public void OnTalkButton()
        {
            if (_player.OnTalk == false)
            {
                _player.OnTalk = true;
            }

            if(UIManager._inst.Talking == false)
                UIManager._inst.CooltimeButton(3.0f, _talkButtonGo);
        }
    }
}