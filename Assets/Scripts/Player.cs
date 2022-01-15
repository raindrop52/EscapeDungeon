using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum CHAR_DIR
    {
        FRONT,
        SIDE,
        BACK,
        COUNT
    }

    public enum CHAR_STATE
    {
        IDLE,
        WALK,
        ATTACK,
    }

    public class Player : Unit
    {
        bool[] _characterDir = new bool[3];
        SpriteRenderer _sprite;
        Animator _anim;

        public float _speed;
        public Transform _playerPosition;        // 플레이어 시작 위치

        [SerializeField] CHAR_DIR _nowDir;      // 플레이어 방향 체크 용
        //[SerializeField] Collider2D _col;     // 레이캐스트 충돌 체크

        [SerializeField] float _x, _y;          // 조이스틱 값 체크
        [SerializeField] Joystick _joystick;
        [SerializeField] LayerMask _layerMask;

        protected override void Start()
        {
            base.Start();

            _sprite = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();

            _characterDir[(int)CHAR_DIR.FRONT] = true;
        }

        protected override void Update()
        {
            base.Update();

            MovePlayer();
        }

        void MovePlayer()
        {
            float absX, absY;

            _x = _joystick.Horizontal;
            _y = _joystick.Vertical;

            absX = Mathf.Abs(_x);
            absY = Mathf.Abs(_y);

            // 가로 이동
            if (absX > absY)
            {
                // 이전에 좌우 이동을 안 했을 경우
                if (_characterDir[(int)CHAR_DIR.SIDE] == false)
                {
                    // 애니메이션 상태 변경
                    ChangeAvatarDir(CHAR_DIR.SIDE);
                }

                if (_x > 0)
                    _sprite.flipX = false;
                else if (_x < 0)
                    _sprite.flipX = true;
            }
            // 세로 이동
            else if (absX < absY)
            {
                if (_sprite.flipX == true)
                {
                    // flip 상태 false 변경
                    _sprite.flipX = false;
                }

                if (_y > 0)
                {
                    // 애니메이션 상태 변경
                    ChangeAvatarDir(CHAR_DIR.BACK);
                }
                else if (_y < 0)
                {
                    // 애니메이션 상태 변경
                    ChangeAvatarDir(CHAR_DIR.FRONT);
                }
            }

            Vector2 dir = new Vector2(_x, _y);

            _x = dir.x;
            _y = dir.y;

            float val = Mathf.Abs(_x) + Mathf.Abs(_y);

            ChangeAnim(CHAR_STATE.WALK, val);

            RaycastHit2D hit;
            Vector2 start = transform.position;
            Vector2 moveValue = dir * _speed * Time.deltaTime;
            float distance = Vector2.Distance(start, start + moveValue);
            hit = Physics2D.Raycast(start, dir, distance, _layerMask);

            if (hit.transform != null)
            {
                return;
            }

            // 캐릭터 이동
            transform.Translate(moveValue);
        }

        // 플레이어 캐릭터 보는 방향 설정
        void ChangeAvatarDir(CHAR_DIR dir)
        {
            for (int i = 0; i < (int)CHAR_DIR.COUNT; i++)
            {
                if(i == (int)dir)
                {
                    _characterDir[i] = true;
                    _nowDir = dir;
                }
                else
                {
                    _characterDir[i] = false;
                }
            }

            ChangeAnim();
        }

        void ChangeAnim(CHAR_STATE state = CHAR_STATE.IDLE, float walkValue = 0)
        {
            if (_anim != null)
            {
                switch (state)
                {
                    case CHAR_STATE.IDLE:
                        {
                            _anim.SetBool("Front_Idle", _characterDir[(int)CHAR_DIR.FRONT]);
                            _anim.SetBool("Side_Idle", _characterDir[(int)CHAR_DIR.SIDE]);
                            _anim.SetBool("Back_Idle", _characterDir[(int)CHAR_DIR.BACK]);
                            break;
                        }
                    case CHAR_STATE.WALK:
                        {
                            _anim.SetFloat("Walk", walkValue);
                            break;
                        }
                    case CHAR_STATE.ATTACK:
                        {
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        public void ChangePlayerPos(Vector2 pos)
        {
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Portal")
            {
                Debug.Log(collision.name + " 포탈 부딪힘");

                // 포탈 스크립트 가져옴
                Portal portalCol = collision.GetComponent<Portal>();
                if(portalCol  != null)
                {
                    GameManager._inst.StartMoveObject(portalCol);
                }
            }
        }
    }
}