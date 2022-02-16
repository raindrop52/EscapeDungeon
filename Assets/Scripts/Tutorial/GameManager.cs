using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public delegate void CallBack();

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;
        
        #region Prefab
        public GameObject _batPrefab;
        public GameObject _expGemPrefab;

        #endregion
        
        public Transform _playerTrans;

        #region GameData
        public GameData_TutorialItem _itemTable;
        #endregion

        private void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            _itemTable = Resources.Load("GameData/GameData_TutorialItem") as GameData_TutorialItem;

            // 플레이어 초기화
            Player player = _playerTrans.GetComponent<Player>();
            player.Init();
            
            // UI매니저 초기화
            UIManager_Tutorial._inst.Init();

            StartCoroutine(_SpawnEnemies());
        }

        IEnumerator _SpawnEnemies()
        {
            while(gameObject.activeSelf && _playerTrans.gameObject != null)
            {
                // 적 랜덤 개체 수
                int randomInt = Random.Range(15, 40);

                for (int i = 0; i < randomInt; i++)
                {
                    Vector2 playerPos = _playerTrans.position;

                    GameObject batObj = Instantiate(_batPrefab);
                    batObj.transform.position = playerPos + Random.insideUnitCircle * 15;
                }

                yield return new WaitForSeconds(10.0f);
            }
        }
        
        void Update()
        {

        }
    }
}