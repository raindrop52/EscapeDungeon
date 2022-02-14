using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;
        
        #region Prefab
        public GameObject _batPrefab;
        public GameObject _expGemPrefab;

        #endregion
        public Transform _playerTrans;
        

        private void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            // �÷��̾� �ʱ�ȭ
            Player player = _playerTrans.GetComponent<Player>();
            player.Init();

            // UI�Ŵ��� �ʱ�ȭ
            UIManager_Tutorial._inst.Init();

            StartCoroutine(_SpawnEnemies());
        }

        IEnumerator _SpawnEnemies()
        {
            while(gameObject.activeSelf && _playerTrans.gameObject != null)
            {
                // �� ���� ��ü ��
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