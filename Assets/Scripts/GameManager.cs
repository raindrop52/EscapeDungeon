using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum EVENT_NAME
    {
        NOEVENT,
        TREASURE,
        TRAP,
        ENEMY_MEET,
        COUNT
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        [Header("�̵� ������Ʈ")]
        [SerializeField] GameObject _moveObject;
        [SerializeField] GameObject _playerObject;

        [Header("�� ��ġ �� ���� ����Ʈ")]
        public List<GameObject> _doorList;
        public List<GameObject> _arrowList;

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

        public void StartMoveObject(Portal portal)
        {
            StartCoroutine(MoveMapObject(portal));
        }

        IEnumerator MoveMapObject(Portal portal)
        {
            // �̵� ��ġ
            float x, y;
            // ȭ���� ����

            // ī�޶� �̵� �� ��Ż�� ���⼺ üũ
            int dir = portal.GoPortal(out x, out y);
            // �̵� ��ġ ������Ʈ ��ġ
            BatchObject(x, y, dir);
            // ��Ȳ ����

            // ȭ�� ����


            yield return null;
        }

        void BatchObject(float x, float y, int dir)
        {
            // Move ������Ʈ ��ǥ �̵�
            Vector3 movePos = _moveObject.transform.position;
            _moveObject.transform.position = new Vector3(movePos.x + x, movePos.y + y, movePos.z);

            // �÷��̾� ������Ʈ ��ǥ �̵�
            Vector3 playerPos = _playerObject.transform.position;
            _playerObject.transform.position = new Vector3(playerPos.x + x, playerPos.y + y, playerPos.z);

            Player player = _playerObject.transform.GetChild(0).GetComponent<Player>();
            if(player != null)
            {
                // �÷��̾� ��ġ�� ȭ��ǥ �������� �̵�
                player.ChangePlayerPos(_arrowList[dir].transform.position);
            }
        }

        void OccurEvent()
        {
            int randNum = Random.Range(0, (int)EVENT_NAME.COUNT*10);

            // No Event 30% - ȸ��, ������ ����
            if(CheckRandomEvent(randNum, 0, 30))
            {
                
            }
            // ���� �߰� 25%
            else if (CheckRandomEvent(randNum, 30, 55))
            {

            }
            // ���� ���� 25%
            else if (CheckRandomEvent(randNum, 55, 80))
            {

            }
            // �� ���� 20%
            else if (CheckRandomEvent(randNum, 80, 100))
            {

            }

        }

        // per�� ����� ������
        bool CheckRandomEvent(int num, int per)
        {
            int maxNum = (int)EVENT_NAME.COUNT * 10;
            int rangeNum = (int)(maxNum * per / 100.0f);

            if(num < rangeNum)
            {
                return true;
            }

            return false;
        }

        // ������ ������ Per ���� ���� ������ ��� true
        bool CheckRandomEvent(int num, int minPer, int maxPer)
        {
            int maxNum = (int)EVENT_NAME.COUNT * 10;
            int rangeMin = (int)(maxNum * minPer / 100.0f);
            int rangeMax = (int)(maxNum * maxPer / 100.0f);

            if(rangeMin <= num && num < rangeMax)
            {
                return true;
            }

            return false;
        }
    }
}