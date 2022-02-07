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
            // ���¸� ���� ����
            _status = STATUS.WAIT;

            // ���������� ������ üũ
            int level = (int)GameManager._inst._stageLevel;

            // ���� �ڷ�ƾ���� �̵�
            StartCoroutine(_ReadyAI(level));

            yield return null;
        }

        bool GoStart()
        {
            // �� ���°� ������ �Ǿ��� ��
            ROOM room = GameManager._inst.Room;

            // ĳ������ �� ���� üũ
            if (GameManager._inst.Room == ROOM.RESTROOM)
                return false;
            else if (GameManager._inst.Room == ROOM.DUNGEON)
                return true;

            return false;
        }

        int errorCnt = 0;
        IEnumerator _ReadyAI(int level)
        {
            // ���¸� �غ�� ����
            _status = STATUS.READY;

            // ���� �Ŵ������� �÷��̾��� �غ� ���� üũ
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
                // stop�� true�� �Ǹ� ���ߵ��� ����
                bool stop = GameManager._inst._clearRoom;

                if (stop)
                    break;
                else
                {
                    time += Time.deltaTime;

                    if (level == (int)STAGE_LV.TRAP)
                    {
                        int randNum = Random.Range(0, maxCnt);
                        // ���� �ð����� ������ ��ġ���� ȭ�� �߻�
                        Cooltime_Shooting(randNum, time);
                        // Ư�� �ð����� ��ü ��ġ���� ȭ�� �߻�
                        All_Shooting(time);
                        // ���� �ð��� ���� �� �߰� ���� ����
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
            // Arrow ������ ����
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