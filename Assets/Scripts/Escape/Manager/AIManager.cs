using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EscapeGame
{
    public enum STATUS
    {
        NONE,
        WAIT,
        READY,
        START,
        END,
    }

    public class AIManager : MonoBehaviour
    {
        public static AIManager _inst;
        [SerializeField] STATUS _status;
        [SerializeField] Arrow_Trap[] _listTrapTrans;

        [Header("스테이지1 함정")]
        [SerializeField] GameObject _tileHidden;

        [SerializeField] int _secArrowShot = 3;
        [SerializeField] bool _randomArrowShot = false;
        [SerializeField] bool _allArrowShot = false;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            ShowHiddenTile(false);

            //_listTrapTrans = transform.Find("Arrow_Trap_List").GetComponentsInChildren<Arrow_Trap>();

            //StartCoroutine(_WaitAI());
        }

        IEnumerator _WaitAI()
        {
            // 상태를 대기로 변경
            _status = STATUS.WAIT;

            // 스테이지의 레벨을 체크
            int level = (int)GameManager._inst._stageLevel;

            // 레디 코루틴으로 이동
            StartCoroutine(_ReadyAI(level));

            yield return null;
        }

        bool GoStart()
        {
            // 방 상태가 던전이 되었을 때
            ROOM room = GameManager._inst.Room;

            // 캐릭터의 방 정보 체크
            if (GameManager._inst.Room == ROOM.RESTROOM)
                return false;
            else if (GameManager._inst.Room == ROOM.DUNGEON)
                return true;

            return false;
        }

        int errorCnt = 0;
        IEnumerator _ReadyAI(int level)
        {
            // 상태를 준비로 변경
            _status = STATUS.READY;

            // 게임 매니저에서 플레이어의 준비 상태 체크
            while (GoStart() == false)
            {
                yield return new WaitForSeconds(0.1f);
            }

            StartCoroutine(_StartAI(level));
        }

        IEnumerator _StartAI(int level)
        {
            _status = STATUS.START;
            float time = 0.0f;
            int maxCnt = _listTrapTrans.Length;

            while (level == GameManager._inst._stageLevel)
            {
                time += Time.deltaTime;

                if (level == 1)
                {
                    int randNum = Random.Range(0, maxCnt);
                    // 일정 시간마다 랜덤한 위치에서 화살 발사
                    if(_randomArrowShot == false)
                        StartCoroutine(_RandomShoot(randNum, _secArrowShot));
                    // 특정 시간마다 전체 위치에서 화살 발사
                    if(_allArrowShot == false)
                        StartCoroutine(_AllShoot(_secArrowShot * 5.0f));
                    // 일정 시간이 지난 후 추가 함정 동작
                }

                yield return new WaitForSeconds(0.01f);
            }

            StartCoroutine(_WaitAI());

            yield return null;
        }

        IEnumerator _RandomShoot(int num, float time)
        {
            // 배열 오버플로 체크
            if (num < _listTrapTrans.Length)
            {
                _randomArrowShot = true;

                // Arrow 프리팹 생성
                GameObject prefab = Resources.Load("Arrow") as GameObject;
                GameObject arrow = Instantiate(prefab);
                arrow.transform.position = _listTrapTrans[num].transform.position;

                yield return new WaitForSeconds(time);

                _randomArrowShot = false;
            }
            else
                yield return null;
        }

        IEnumerator _AllShoot(float time)
        {
            _allArrowShot = true;

            while(_randomArrowShot == true)
            {
                // 동시 발사를 방지하기 위한 대기
                yield return new WaitForSeconds(0.01f);
            }

            // Arrow 프리팹 생성
            GameObject prefab = Resources.Load("Arrow") as GameObject;

            foreach (Arrow_Trap tp in _listTrapTrans)
            {
                GameObject arrow = Instantiate(prefab);
                arrow.transform.position = tp.gameObject.transform.position;
                yield return null;
            }

            yield return new WaitForSeconds(time);

            _allArrowShot = false;
        }

        // 히든 타일 설정
        public void ShowHiddenTile(bool show)
        {
            Tilemap[] tileHidden = _tileHidden.GetComponentsInChildren<Tilemap>(true);
            foreach (Tilemap tile in tileHidden)
            {
                TilemapCollider2D collider = tile.GetComponent<TilemapCollider2D>();

                // 콜라이더가 있는 벽 
                if (collider != null)
                {
                    tile.gameObject.SetActive(!show);
                }
                else
                {
                    tile.gameObject.SetActive(show);
                }
            }
        }
    }
}