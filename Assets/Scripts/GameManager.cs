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
            // 상황 조성

            // 화면 공개


            yield return null;
        }

        void BatchObject(float x, float y, int dir)
        {
            // Move 오브젝트 좌표 이동
            Vector3 movePos = _moveObject.transform.position;
            _moveObject.transform.position = new Vector3(movePos.x + x, movePos.y + y, movePos.z);

            // 플레이어 오브젝트 좌표 이동
            Vector3 playerPos = _playerObject.transform.position;
            _playerObject.transform.position = new Vector3(playerPos.x + x, playerPos.y + y, playerPos.z);

            Player player = _playerObject.transform.GetChild(0).GetComponent<Player>();
            if(player != null)
            {
                // 플레이어 위치를 화살표 방향으로 이동
                player.ChangePlayerPos(_arrowList[dir].transform.position);
            }
        }

        void OccurEvent()
        {
            int randNum = Random.Range(0, (int)EVENT_NAME.COUNT*10);

            // No Event 30% - 회복, 아이템 정비
            if(CheckRandomEvent(randNum, 0, 30))
            {
                
            }
            // 보물 발견 25%
            else if (CheckRandomEvent(randNum, 30, 55))
            {

            }
            // 함정 출현 25%
            else if (CheckRandomEvent(randNum, 55, 80))
            {

            }
            // 적 조우 20%
            else if (CheckRandomEvent(randNum, 80, 100))
            {

            }

        }

        // per는 백분율 단위로
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

        // 지정한 범위의 Per 내에 값이 존재할 경우 true
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