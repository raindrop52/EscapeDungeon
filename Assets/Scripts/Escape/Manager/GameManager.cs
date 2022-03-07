using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public enum ROOM
    {
        RESTROOM,
        DUNGEON,

    }

    public delegate void Callback();

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        public Transform _arrowParent;
        public GameObject _directLight;

        [Header("��������")]
        public int _stageLevel = 0;         // ���� �������� ����
        //[SerializeField] ROOM _room;        // ���� ������ ��
        //public ROOM Room
        //{
        //    get { return _room; }
        //}
        Transform _spawnPos;         // �ʱ� ���� ��ġ
        public Player _player;              // �÷��̾�

        [Header("�� ��ȯ ���ø�")]
        [SerializeField] Image _loadingPanel;

        bool _mapMove = false;
        public bool MapMoving
        {
            get { return _mapMove; }
            set { _mapMove = value; }
        }

        bool _die = false;
        public bool Die
        { get { return _die; } set { _die = value; } }

        public bool _editorMode = true;

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            if (_editorMode == false)
            {
                if (_directLight != null)
                {
                    _directLight.SetActive(false);
                }
            }

            // ���� ��ġ�� �޽Ĺ�
            _spawnPos = transform.Find("SpawnPos").GetComponent<Transform>();
            SetSpawnPos(_spawnPos.position);

            // �÷��̾� �ʱ�ȭ
            _player.Init();

            // UI�Ŵ��� �ʱ�ȭ
            UIManager._inst.Init();
            // AI�Ŵ��� �ʱ�ȭ
            StageManager._inst.Init();
        }

        void Update()
        {
            if (_directLight != null)
            {
                _directLight.SetActive(_editorMode);
            }
        }

        public void PlayInit()
        {
            // �÷��̾� ũ�� ����
            _player.CheckScale();

            // �÷��̾� ��ġ ����
            _player.gameObject.transform.position = _spawnPos.position;
            _die = false;
            CheckDie();
        }

        public void SetSpawnPos(Vector3 pos)
        {
            _spawnPos.position = pos;
        }

        public void CheckDie()
        {
            StartCoroutine(_CheckDie());
        }

        public void StartMoveStage()
        {
            StartCoroutine(MoveStart());
        }

        IEnumerator MoveStart()
        {
            if (_mapMove == false)
            {
                _mapMove = true;

                // ȭ���� ����
                ShowLoading(true);

                yield return new WaitForSeconds(0.5f);

                StartCoroutine(MoveStage());
            }
            else
            {
                Debug.Log("�̵� �Ұ�");
                yield return null;
            }
        }

        IEnumerator MoveStage()
        {
            if(CameraManager._inst.MoveTarget == false)
                CameraManager._inst.MoveTarget = true;

            yield return new WaitForSeconds(1.0f);

            if (CameraManager._inst.MoveTarget == true)
                CameraManager._inst.MoveTarget = false;

            // ȭ�� ����
            StartCoroutine(MoveEnd());
        }

        IEnumerator MoveEnd()
        {
            ShowLoading(false);

            yield return new WaitForSeconds(0.1f);

            _mapMove = false;
        }

        void ShowLoading(bool show)
        {
            if(show)
            {
                StartCoroutine(_FadeOut(0.5f));
            }
            else
            {
                StartCoroutine(_FadeIn(1.0f));
            }
        }

        IEnumerator _FadeIn(float time)
        {
            Color color = _loadingPanel.color;

            while(color.a > 0f)
            {
                color.a -= Time.deltaTime / time;
                _loadingPanel.color = color;
                yield return null;
            }
        }

        IEnumerator _FadeOut(float time)
        {
            Color color = _loadingPanel.color;

            while (color.a <= 1.0f)
            {
                color.a += Time.deltaTime / time;
                _loadingPanel.color = color;
                yield return null;
            }
        }

        IEnumerator _CheckDie()
        {
            while(true)
            {
                if(_die == true)
                {
                    UIManager._inst.NowUI = UI_ID.GAMEOVER;
                    UIManager._inst.ChangeUI();
                    break;
                }

                yield return null;
            }
        }
    }
}