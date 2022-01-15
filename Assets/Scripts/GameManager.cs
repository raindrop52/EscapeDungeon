using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
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
            // ȭ�� ����

            yield return null;
        }

        void BatchObject(float x, float y, int dir)
        {
            // Move ������Ʈ ��ǥ �̵�
            Vector3 movePos = _moveObject.transform.position;
            _moveObject.transform.position = new Vector3(movePos.x + x, movePos.y + y, movePos.z);

            // Player ������Ʈ ��ǥ �̵�
            Vector3 playerPos = _playerObject.transform.position;
            _playerObject.transform.position = new Vector3(playerPos.x + x, playerPos.y + y, playerPos.z);

            Player player = _playerObject.transform.GetChild(0).GetComponent<Player>();
            if(player != null)
            {
                player.ChangePlayerPos(_arrowList[dir].transform.position);
            }
        }
    }
}