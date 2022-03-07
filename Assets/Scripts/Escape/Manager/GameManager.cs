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

        [Header("스테이지")]
        public int _stageLevel = 0;         // 현재 스테이지 레벨
        //[SerializeField] ROOM _room;        // 현재 진입한 방
        //public ROOM Room
        //{
        //    get { return _room; }
        //}
        Transform _spawnPos;         // 초기 시작 위치
        public Player _player;              // 플레이어

        [Header("맵 전환 템플릿")]
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

            // 최초 위치는 휴식방
            _spawnPos = transform.Find("SpawnPos").GetComponent<Transform>();
            SetSpawnPos(_spawnPos.position);

            // 플레이어 초기화
            _player.Init();

            // UI매니저 초기화
            UIManager._inst.Init();
            // AI매니저 초기화
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
            // 플레이어 크기 설정
            _player.CheckScale();

            // 플레이어 위치 설정
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

                // 화면을 가림
                ShowLoading(true);

                yield return new WaitForSeconds(0.5f);

                StartCoroutine(MoveStage());
            }
            else
            {
                Debug.Log("이동 불가");
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

            // 화면 공개
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