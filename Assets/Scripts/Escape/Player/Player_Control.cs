using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_Control : Unit
    {
        BoxCollider2D _boxCol;          // �浹 �˻�
        public LayerMask _layerMask;    // ��� �Ұ� ������Ʈ ����
        SpriteRenderer _sprite;
        Animator _anim;
        Player _player;

        Vector3 vector;

        [Header("ĳ���� �̵� ����")]
        [SerializeField] float _speed;
        public float Speed
        { get { return _speed; } }
        public int _walkCount;
        [SerializeField] int _currentWalkCount;
        public bool _canMove = true;

        [Header("��Ÿ")]
        public PlayerLight _light;
        public Transform _playerPosition;        // �÷��̾� ���� ��ġ

        [Header("���̽�ƽ")]
        [SerializeField] float _x, _y;          // ���̽�ƽ �� üũ
        [SerializeField] Joystick _joystick;
        [SerializeField] GameObject _talkButtonGo;

        [Header("�ߵ� ����")]
        float _slowSpeed;                     // Slow �̵��ӵ�
        public float SlowSpeed
        { get { return _slowSpeed; } set { _slowSpeed = value; } }

        [SerializeField] bool _joystickMove = false;

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
                // �ߵ�:���ο쿡 �ɸ� ���
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
                // �̵� ���� ����
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

            float x, y;

            if (_joystickMove == false)
            {
                // Ű����
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                x = _joystick.Horizontal;
                y = _joystick.Vertical;
            }     

            if (x != 0 || y != 0)
                result = true;

            _x = x;
            _y = y;

            return result;
        }

        public void OnTalkButton()
        {
            if(_player._btnDo == false)
                _player._btnDo = true;

            // ��ȭ ���� ���� ���� ��Ÿ��
            if (UIManager._inst.Talking == false)
            {
                // Ʃ�丮�� ����̸�, �ؽ�Ʈ �޽��� �ڽ��� ǥ�õ� ���
                if(GameManager._inst._player._useTutorial == true && UIManager._inst._messageBox.gameObject.activeSelf == true)
                {
                    // �������� �ʰ� �Ѿ
                    return;
                }

                UIManager._inst.CooltimeButton(3.0f, _talkButtonGo);
            }
        }
    }
}