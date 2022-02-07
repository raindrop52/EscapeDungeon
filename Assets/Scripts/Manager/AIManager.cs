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
        [SerializeField] bool _allShooting = false;

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
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
            StartCoroutine(_StartAI(level));
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

                    if (level == (int)STAGE_LV.TRAP)
                    {
                        int randNum = Random.Range(0, maxCnt);
                        // 일정 시간마다 랜덤한 위치에서 화살 발사
                        Cooltime_Shooting(randNum, time);
                        // 특정 시간마다 전체 위치에서 화살 발사
                        All_Shooting(time);
                        // 일정 시간이 지난 후 추가 함정 동작
                    }
                }

                yield return null;
            }

            StartCoroutine(_WaitAI());

            yield return null;
        }

        void Cooltime_Shooting(int num, float time)
        {
            if (_shooting == true)
                return;

            int timeCheck = (int)(time % _secArrowShot);

            if (timeCheck == 0 && time > 1)
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

        void All_Shooting(float time)
        {
            if (_allShooting == true)
                return;

            int timeCheck = (int)(time % (_secArrowShot * 3.5));

            if (timeCheck == 0 && time > 1)
            {
                _allShooting = true;
                StartCoroutine(_AllShoot());
            }
        }

        IEnumerator _AllShoot()
        {
            // Arrow 프리팹 생성
            GameObject prefab = Resources.Load("Arrow") as GameObject;

            foreach (Trap_Pos tp in _listTrapTrans)
            {
                GameObject arrow = Instantiate(prefab);
                arrow.transform.position = tp.gameObject.transform.position;
                yield return null;
            }

            yield return new WaitForSeconds(2.0f);

            _allShooting = false;
        }
    }
}