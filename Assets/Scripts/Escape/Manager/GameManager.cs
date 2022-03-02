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
        Transform _startPos;         // �ʱ� ���� ��ġ
        public Player _player;              // �÷��̾�

        [Header("�� ��ȯ ���ø�")]
        [SerializeField] Image _loadingPanel;

        bool _mapMove = false;
        public bool MapMoving
        {
            get { return _mapMove; }
            set { _mapMove = value; }
        }

        public bool _editorMode = true;

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            _startPos = transform.Find("StartPos").GetComponent<Transform>();

            // �÷��̾� �ʱ�ȭ
            _player.Init();
            // �ʱ� �÷��̾� ��ġ ���� (RestRoom)
            _player.gameObject.transform.position = _startPos.position;

            if (_editorMode == false)
            {
                if (_directLight != null)
                {
                    _directLight.SetActive(false);
                }
            }

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
    }
}