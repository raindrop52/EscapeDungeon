using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        static AIManager _inst;
        [SerializeField] STATUS _status;
        [SerializeField] Trap_Pos[] _listTrapTrans;
        
        [Header("스테이지1 함정")]
        [SerializeField] int _secArrowShot = 3;
        [SerializeField] bool _shooting = false;

        private void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            _listTrapTrans = GetComponentsInChildren<Trap_Pos>();

            StartCoroutine(_WaitAI());
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

        int errorCnt = 0;
        IEnumerator _ReadyAI(int level)
        {
            // 상태를 준비로 변경
            if(_status == STATUS.WAIT)
                _status = STATUS.READY;

            // 게임 매니저에서 플레이어의 준비 상태 체크
            bool start = false;
            while (true)
            {
                // 캐릭터의 방 정보 체크
                if (GameManager._inst.Room == ROOM.RESTROOM)
                    start = false;
                else if (GameManager._inst.Room == ROOM.DUNGEON)
                    start = true;

                if(start)
                {
                    break;
                }
                else
                {
                    if(errorCnt> 10)
                    {
                        Debug.Log("에러 : 던전 상태 변경 오류. 강제 탈출");
                        break;
                    }

                    errorCnt++;
                    yield return new WaitForSeconds(0.1f);
                }
            }

            if(errorCnt > 10)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(_StartAI(level));
            }
        }

        IEnumerator _StartAI(int level)
        {
            _status = STATUS.START;
            float time = 0.0f;
            int maxCnt = _listTrapTrans.Length;

            while (time <= 120.0f)
            {
                // stop이 true가 되면 멈추도록 설정
                bool stop = GameManager._inst._clearRoom;

                if (stop)
                    break;
                else
                {
                    time += Time.deltaTime;

                    if(level == (int)STAGE_LV.TRAP)
                    {
                        int randNum = Random.Range(0, maxCnt);
                        // 일정 시간마다 랜덤한 위치에서 화살 발사
                        Cooltime_Shooting(randNum, time);

                        // 특정 시간마다 전체 위치에서 화살 발사

                        // 일정 시간이 지난 후 추가 함정 동작
                    }
                }
            }

            StartCoroutine(_WaitAI());

            yield return null;
        }

        void Cooltime_Shooting(int num, float time)
        {
            if (_shooting == true)
                return;

            int timeCheck = (int)(time % _secArrowShot);

            if (timeCheck == 0)
            {
                _shooting = true;
                StartCoroutine(_RandomShoot(num));
            }
        }

        IEnumerator _RandomShoot(int num)
        {
            // 배열 오버플로 체크
            if (num < _listTrapTrans.Length)
            {
                // Arrow 프리팹 생성
                GameObject prefab = Resources.Load("Arrow") as GameObject;
                GameObject arrow = Instantiate(prefab);
                arrow.transform.position = _listTrapTrans[num].transform.position;

                yield return new WaitForSeconds(1.0f);

                _shooting = false;
            }

            yield return null;
        }
    }
}