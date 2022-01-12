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
        public float _speed;
        bool[] _characterDir = new bool[3];
        [SerializeField] int _nowDir = 0;
        SpriteRenderer _sprite;
        Animator _anim;

        [SerializeField] Joystick _joystick;

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
            float x = _joystick.Horizontal;
            float y = _joystick.Vertical;

            // 상하 이동
            if (y != 0)
            {
                if (y > 0)
                {
                    // 애니메이션 상태 변경
                    ChangeAvatarDir(CHAR_DIR.BACK);
                }
                else if (y < 0)
                {
                    // 애니메이션 상태 변경
                    ChangeAvatarDir(CHAR_DIR.FRONT);
                }
            }
            // 좌우 이동
            else if (x != 0)
            {
                // 이전에 좌우 이동을 안 했을 경우
                if(_characterDir[(int)CHAR_DIR.SIDE] == false)
                {
                    // 애니메이션 상태 변경
                    ChangeAvatarDir(CHAR_DIR.SIDE);
                }

                if (x > 0)
                    _sprite.flipX = false;
                else
                    _sprite.flipX = true;
            }

            Vector2 dir = new Vector2(x, y);

            float val = Mathf.Abs(x) + Mathf.Abs(y);

            ChangeAnim(CHAR_STATE.WALK, val);

            transform.Translate(dir * _speed * Time.deltaTime);
        }

        void ChangeAvatarDir(CHAR_DIR dir)
        {
            for (int i = 0; i < (int)CHAR_DIR.COUNT; i++)
            {
                if(i == (int)dir)
                {
                    _characterDir[i] = true;
                    _nowDir = i;
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
    }
}