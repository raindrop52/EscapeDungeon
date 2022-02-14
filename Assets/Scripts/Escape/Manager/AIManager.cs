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

        [Header("��������1 ����")]
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

            while (level == GameManager._inst._stageLevel)
            {
                time += Time.deltaTime;

                if (level == 1)
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

            foreach (Arrow_Trap tp in _listTrapTrans)
            {
                GameObject arrow = Instantiate(prefab);
                arrow.transform.position = tp.gameObject.transform.position;
                yield return null;
            }

            yield return new WaitForSeconds(time);

            _allArrowShot = false;
        }

        // ���� Ÿ�� ����
        public void ShowHiddenTile(bool show)
        {
            Tilemap[] tileHidden = _tileHidden.GetComponentsInChildren<Tilemap>(true);
            foreach (Tilemap tile in tileHidden)
            {
                TilemapCollider2D collider = tile.GetComponent<TilemapCollider2D>();

                // �ݶ��̴��� �ִ� �� 
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