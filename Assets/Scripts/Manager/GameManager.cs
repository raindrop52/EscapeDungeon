using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public enum STAGE_LV
    {
        NONE,
        TRAP,

    }

    public enum ROOM
    {
        RESTROOM,
        DUNGEON,

    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        public GameObject _directLight;

        [Header("스테이지")]
        public STAGE_LV _stageLevel;        // 현재 스테이지 레벨
        [SerializeField] ROOM _room;        // 현재 진입한 방
        public ROOM Room
        {
            get { return _room; }
        }
        public List<Transform> _stagePosList;   // 스테이지별 플레이어 스폰 위치
        public Player _player;                  // 플레이어

        [Header("맵 전환 템플릿")]
        [SerializeField] Image _loadingPanel;

        bool _mapMove = false;
        public bool MapMoving
        {
            get { return _mapMove; }
            set { _mapMove = value; }
        }

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            // 초기 플레이어 위치 설정
            _player.gameObject.transform.position = _stagePosList[0].position;

            if(_directLight != null)
            {
                _directLight.SetActive(false);
            }
        }


        void Update()
        {

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
            if (_room == ROOM.RESTROOM)
            {
                _room = ROOM.DUNGEON;
                _stageLevel = STAGE_LV.TRAP;
            }

            // 캐릭터 배치
            Vector3 pos = _stagePosList[(int)_room].transform.position;

            if (_player != null)
                _player.ChangePlayerPos(pos);

            CameraManager._inst.ChangeCam();

            yield return new WaitForSeconds(1.0f);

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
    }
}