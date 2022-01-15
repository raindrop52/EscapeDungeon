using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        [Header("이동 오브젝트")]
        [SerializeField] GameObject _moveObject;
        [SerializeField] GameObject _playerObject;

        [Header("맵 배치 시 숨길 리스트")]
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
            // 이동 수치
            float x, y;
            // 화면을 가림

            // 카메라 이동 및 포탈의 방향성 체크
            int dir = portal.GoPortal(out x, out y);
            // 이동 위치 오브젝트 배치
            BatchObject(x, y, dir);
            // 화면 공개

            yield return null;
        }

        void BatchObject(float x, float y, int dir)
        {
            // Move 오브젝트 좌표 이동
            Vector3 movePos = _moveObject.transform.position;
            _moveObject.transform.position = new Vector3(movePos.x + x, movePos.y + y, movePos.z);

            // Player 오브젝트 좌표 이동
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