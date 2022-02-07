using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum STAGE_LV
    {
        NONE,
        TRAP,
        DEFENSE,
        BOSS,

    }

    public enum ROOM
    {
        RESTROOM,
        DUNGEON,

    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        [Header("스테이지")]
        public STAGE_LV _stageLevel;        // 현재 스테이지 레벨
        [SerializeField] ROOM _room;        // 현재 진입한 방
        public ROOM Room
        {
            get { return _room; }
        }
        public bool _clearRoom = false;     // 방 클리어 상태
                
        [Header("맵 전환 템플릿")]
        [SerializeField] SpriteRenderer _loadingPanel;

        bool mapMove = false;
        public bool MapMoving
        {
            get { return mapMove; }
            set { mapMove = value; }
        }

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {

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
            if (mapMove == false)
            {
                mapMove = true;

                // 화면을 가림
                ShowLoading(true);

                StartCoroutine(MoveStage());

                yield return null;
            }
            else
            {
                Debug.Log("이동 불가");
                yield return null;
            }
        }

        IEnumerator MoveStage()
        {
            // 맵 전환

            // 캐릭터 배치
            
            yield return new WaitForSeconds(1.0f);

            // 화면 공개
            StartCoroutine(MoveEnd());
        }

        IEnumerator MoveEnd()
        {
            mapMove = false;

            ShowLoading(false);

            yield return null;
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