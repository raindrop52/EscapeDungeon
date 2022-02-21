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

        public float _enemySpawnRange = 15f;
        public int _maxEnemyCount = 50;
        List<Enemy> _enemyList = new List<Enemy>();
        public List<Enemy> EnemyList
        {
            get { return _enemyList; }
        }

        private void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            _itemTable = Resources.Load("GameData/GameData_TutorialItem") as GameData_TutorialItem;

            // �÷��̾� �ʱ�ȭ
            Player player = _playerTrans.GetComponent<Player>();
            player.Init();
            
            // UI�Ŵ��� �ʱ�ȭ
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
            // WaitUnil --> _gameStart ������ true�� �� ������ ���
            // 3���� ���� ��� ���� ���
            yield return new WaitUntil(() => _gameStart == true);     // ���ٽ�
            //yield return new WaitUntil(delegate() { return (_gameStart == true); });
            //yield return new WaitUntil(IsGameStart);

            while (gameObject.activeSelf && _playerTrans.gameObject != null)
            {
                // ���� �ʵ忡 �ִ� �� ���ʹ� ������ üũ, �ִ� ������ ������ �������� �ʰ� ���
                if (_enemyList.Count > _maxEnemyCount)
                {
                    yield return null;
                    continue;
                }

                // �� ���� ��ü ��
                int randomInt = Random.Range(20, 40);

                for (int i = 0; i < randomInt; i++)
                {
                    Vector2 playerPos = _playerTrans.position;

                    GameObject batObj = Instantiate(_batPrefab);

                    Vector2 randomVector = Random.insideUnitCircle * 15;

                    float halfRange = _enemySpawnRange * 0.5f;

                    if(randomVector.x >= 0.0f)
                        randomVector.x = Mathf.Max(randomVector.x, halfRange);
                    if (randomVector.y >= 0.0f)
                        randomVector.y = Mathf.Max(randomVector.y, halfRange);
                    if (randomVector.x < 0.0f)
                        randomVector.x = Mathf.Min(randomVector.x, -1 * halfRange);
                    if (randomVector.y < 0.0f)
                        randomVector.y = Mathf.Min(randomVector.y, -1 * halfRange);

                    batObj.transform.position = playerPos + randomVector;

                    Enemy enemy = batObj.GetComponent<Enemy>();
                    _enemyList.Add(enemy);
                }

                yield return new WaitForSeconds(10.0f);
            }
        }
    }
}