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
        [SerializeField] bool _randomArrowShot = false;
        [SerializeField] bool _allArrowShot = false;

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
                yield return new WaitForSeconds(0.1f);
            }

            StartCoroutine(_StartAI(level));
        }

        IEnumerator _StartAI(int level)
        {
            _status = STATUS.START;
            float time = 0.0f;
            int maxCnt = _listTrapTrans.Length;

            while (level == (int)GameManager._inst._stageLevel)
            {
                time += Time.deltaTime;

                if (level == (int)STAGE_LV.TRAP)
                {
                    int randNum = Random.Range(0, maxCnt);
                    // ���� �ð����� ������ ��ġ���� ȭ�� �߻�
                    if(_randomArrowShot == false)
                        StartCoroutine(_RandomShoot(randNum, _secArrowShot));
                    // Ư�� �ð����� ��ü ��ġ���� ȭ�� �߻�
                    if(_allArrowShot == false)
                        StartCoroutine(_AllShoot(_secArrowShot * 5.0f));
                    // ���� �ð��� ���� �� �߰� ���� ����
                }

                yield return new WaitForSeconds(0.01f);
            }

            StartCoroutine(_WaitAI());

            yield return null;
        }

        IEnumerator _RandomShoot(int num, float time)
        {
            // �迭 �����÷� üũ
            if (num < _listTrapTrans.Length)
            {
                _randomArrowShot = true;

                // Arrow ������ ����
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
                // ���� �߻縦 �����ϱ� ���� ���
                yield return new WaitForSeconds(0.01f);
            }

            // Arrow ������ ����
            GameObject prefab = Resources.Load("Arrow") as GameObject;

            foreach (Trap_Pos tp in _listTrapTrans)
            {
                GameObject arrow = Instantiate(prefab);
                arrow.transform.position = tp.gameObject.transform.position;
                yield return null;
            }

            yield return new WaitForSeconds(time);

            _allArrowShot = false;
        }
    }
}