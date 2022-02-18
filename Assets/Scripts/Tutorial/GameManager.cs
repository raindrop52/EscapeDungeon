using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public delegate void CallBack();

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;
        public Transform _playerTrans;
        
        #region Prefab
        public GameObject _batPrefab;
        public GameObject _expGemPrefab;

        #endregion
        #region UI

        #endregion
        #region GameData
        public GameData_TutorialItem _itemTable;
        #endregion

        bool _gameStart = false;
        public bool GameStart
        { get { return _gameStart; } set { _gameStart = value; } }

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

            GameObject ui = UIManager_Tutorial._inst._introUI;
            if(ui != null)
            {
                Transform btnTrans = ui.transform.Find("GameStartButton");

                if(btnTrans != null)
                {
                    Button startBtn = btnTrans.GetComponent<Button>();

                    if(startBtn != null)
                    {
                        startBtn.onClick.AddListener(() =>
                        {
                            _gameStart = true;

                            UIManager_Tutorial._inst.CloseAllUI();
                            UIManager_Tutorial._inst._playUI.SetActive(true);

                        });
                    }
                }
            }

            UIManager_Tutorial._inst.CloseAllUI();
            UIManager_Tutorial._inst._introUI.SetActive(true);

            StartCoroutine(_SpawnEnemies());
        }

        /*
        bool IsGameStart()
        {
            return (_gameStart == true);
        }*/

        IEnumerator _SpawnEnemies()
        {
            // WaitUnil --> _gameStart 변수가 true가 될 때까지 대기
            // 3개의 문법 모두 같은 기능
            yield return new WaitUntil(() => _gameStart == true);     // 람다식
            //yield return new WaitUntil(delegate() { return (_gameStart == true); });
            //yield return new WaitUntil(IsGameStart);

            while (gameObject.activeSelf && _playerTrans.gameObject != null)
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