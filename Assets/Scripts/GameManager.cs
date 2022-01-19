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

    public class GameManager : MonoBehaviour, IRandom
    {
        public static GameManager _inst;

        [Header("�̵� ������Ʈ")]
        [SerializeField] GameObject _moveObject;
        [SerializeField] GameObject _playerObject;

        [Header("�� ��ġ �� ���� ������Ʈ")]
        public List<GameObject> _doorList;
        public List<GameObject> _portalMovePosList;

        [Header("�� ��ȯ ���ø�")]
        [SerializeField] SpriteRenderer _loadingPanel;

        bool move = false;
        public bool Moving
        {
            get { return move; }
            set { move = value; }
        }

        Vector2 _camToObj = Vector2.zero;

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            Vector2 camPos = Camera.main.transform.position;
            _camToObj = new Vector2(camPos.x, camPos.y);
        }


        void Update()
        {

        }

        public void StartMoveObject(Portal portal)
        {
            StartCoroutine(MoveStart(portal));
        }

        IEnumerator MoveStart(Portal portal)
        {
            if (move == false)
            {
                move = true;

                // ȭ���� ����
                ShowLoading(true);

                StartCoroutine(MoveMapObject(portal));

                yield return null;
            }
            else
            {
                Debug.Log("�̵� �Ұ�");
                yield return null;
            }
        }

        IEnumerator MoveMapObject(Portal portal)
        {
            // �̵� ��ġ
            float x, y;
            yield return new WaitForSeconds(1.0f);
            
            // ī�޶� �̵� �� ��Ż�� ���⼺ üũ
            int dir = portal.GoPortal(out x, out y);
            
            // �̵� ��ġ ������Ʈ ��ġ
            BatchObject(x, y, dir);
            
            // ��Ȳ ����
            OccurEvent();
            yield return new WaitForSeconds(1.0f);

            // ȭ�� ����
            StartCoroutine(MoveEnd());
        }

        IEnumerator MoveEnd()
        {
            move = false;

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

        void BatchObject(float x, float y, int dir)
        {
            // Move ������Ʈ ��ǥ �̵�
            Vector3 movePos = _moveObject.transform.localPosition;
            _moveObject.transform.localPosition = new Vector3(movePos.x + x, movePos.y + y, movePos.z);

            // �÷��̾� ������Ʈ ��ǥ �̵�
            Vector3 playerPos = _playerObject.transform.localPosition;
            _playerObject.transform.localPosition = new Vector3(playerPos.x + x, playerPos.y + y, playerPos.z);

            //// Move ������Ʈ ��ǥ �̵�
            //Vector3 movePos = _moveObject.transform.position;
            //_moveObject.transform.position = new Vector3(movePos.x + x, movePos.y + y, movePos.z);

            //// �÷��̾� ������Ʈ ��ǥ �̵�
            //Vector3 playerPos = _playerObject.transform.position;
            //_playerObject.transform.position = new Vector3(playerPos.x + x, playerPos.y + y, playerPos.z);

            Player player = _playerObject.transform.GetChild(0).GetComponent<Player>();
            if(player != null)
            {
                // �÷��̾� ��ġ�� ȭ��ǥ �������� �̵�
                player.ChangePlayerPos(_portalMovePosList[dir].transform.position);
            }
        }

        void OccurEvent()
        {
            int randNum = Random.Range(0, (int)EVENT_NAME.COUNT*10);

            // No Event 30% - (ȸ��, ������ ����) / (Ż�ⱸ)
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
        public bool CheckRandomEvent(int num, int per)
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
        public bool CheckRandomEvent(int num, int minPer, int maxPer)
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