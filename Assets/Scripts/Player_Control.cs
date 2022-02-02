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
        [Header("캐릭터 이동 관련")]
        public float _speed;
        public int _walkCount;
        [SerializeField] int _currentWalkCount;
        public bool _canMove = true;
        [Header("캐릭터 대쉬 관련")]
        public int _dashCount;
        [SerializeField] int _currentDashCount;
        public float _dashSpeed;
        public bool _canDash = true;
        [Header("캐릭터 공격 관련")]
        public bool _canAttack = true;

        [Header("기타")]
        public PlayerLight _light;
        public Transform _playerPosition;        // 플레이어 시작 위치

        [SerializeField] CHAR_DIR _nowDir;      // 플레이어 방향 체크 용

        [SerializeField] float _x, _y;          // 조이스틱 값 체크
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
                Debug.Log("대쉬 중 " + _currentDashCount);
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
            
            // 이동 가능 상태
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
                // 버튼 클릭 시 대쉬
            }
            else
            {
                // 스페이스바 눌릴 시 대쉬
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
                // 버튼 클릭 시 공격
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

            // 포탈 충돌체에 부딛히고, 이동상태가 아닌 경우
            if (collision.tag == "Portal" && onMoving == false)
            {
                Debug.Log(collision.name + " 포탈 부딪힘");

                // 포탈 스크립트 가져옴
                Portal portalCol = collision.GetComponent<Portal>();
                if (portalCol != null)
                {
                    GameManager._inst.StartMoveObject(portalCol);
                }
            }
        }
    }
}