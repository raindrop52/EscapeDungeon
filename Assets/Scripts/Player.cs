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
        public Transform _playerPosition;        // �÷��̾� ���� ��ġ

        [SerializeField] CHAR_DIR _nowDir;      // �÷��̾� ���� üũ ��
        //[SerializeField] Collider2D _col;     // ����ĳ��Ʈ �浹 üũ

        [SerializeField] float _x, _y;          // ���̽�ƽ �� üũ
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

            // ���� �̵�
            if (absX > absY)
            {
                // ������ �¿� �̵��� �� ���� ���
                if (_characterDir[(int)CHAR_DIR.SIDE] == false)
                {
                    // �ִϸ��̼� ���� ����
                    ChangeAvatarDir(CHAR_DIR.SIDE);
                }

                if (_x > 0)
                    _sprite.flipX = false;
                else if (_x < 0)
                    _sprite.flipX = true;
            }
            // ���� �̵�
            else if (absX < absY)
            {
                if (_sprite.flipX == true)
                {
                    // flip ���� false ����
                    _sprite.flipX = false;
                }

                if (_y > 0)
                {
                    // �ִϸ��̼� ���� ����
                    ChangeAvatarDir(CHAR_DIR.BACK);
                }
                else if (_y < 0)
                {
                    // �ִϸ��̼� ���� ����
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

            // ĳ���� �̵�
            transform.Translate(moveValue);
        }

        // �÷��̾� ĳ���� ���� ���� ����
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
                Debug.Log(collision.name + " ��Ż �ε���");

                // ��Ż ��ũ��Ʈ ������
                Portal portalCol = collision.GetComponent<Portal>();
                if(portalCol  != null)
                {
                    GameManager._inst.StartMoveObject(portalCol);
                }
            }
        }
    }
}