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
        
        [Header("��������1 ����")]
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
            // ���¸� ���� ����
            _status = STATUS.WAIT;

            // ���������� ������ üũ
            int level = (int)GameManager._inst._stageLevel;

            // ���� �ڷ�ƾ���� �̵�
            StartCoroutine(_ReadyAI(level));

            yield return null;
        }

        int errorCnt = 0;
        IEnumerator _ReadyAI(int level)
        {
            // ���¸� �غ�� ����
            if(_status == STATUS.WAIT)
                _status = STATUS.READY;

            // ���� �Ŵ������� �÷��̾��� �غ� ���� üũ
            bool start = false;
            while (true)
            {
                // ĳ������ �� ���� üũ
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
                        Debug.Log("���� : ���� ���� ���� ����. ���� Ż��");
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
                // stop�� true�� �Ǹ� ���ߵ��� ����
                bool stop = GameManager._inst._clearRoom;

                if (stop)
                    break;
                else
                {
                    time += Time.deltaTime;

                    if(level == (int)STAGE_LV.TRAP)
                    {
                        int randNum = Random.Range(0, maxCnt);
                        // ���� �ð����� ������ ��ġ���� ȭ�� �߻�
                        Cooltime_Shooting(randNum, time);

                        // Ư�� �ð����� ��ü ��ġ���� ȭ�� �߻�

                        // ���� �ð��� ���� �� �߰� ���� ����
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
            // �迭 �����÷� üũ
            if (num < _listTrapTrans.Length)
            {
                // Arrow ������ ����
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