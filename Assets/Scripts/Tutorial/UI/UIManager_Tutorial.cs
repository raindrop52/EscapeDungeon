using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class UIManager_Tutorial : MonoBehaviour
    {
        public static UIManager_Tutorial _inst;

        public GameObject _damageTextPrefab;

        public GameObject _introUI;
        public GameObject _playUI;
        public GameObject _gameOverUI;

        public LevelUpUI _levelUpUI;
        public CollectionUI _collectionUI;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            _introUI = transform.Find("IntroUI").gameObject;
            _playUI = transform.Find("PlayUI").gameObject;
            _gameOverUI = transform.Find("GameOverUI").gameObject;

            ExpBar expBar = _playUI.GetComponentInChildren<ExpBar>();
            expBar.Init();
                        
            _levelUpUI = _playUI.GetComponentInChildren<LevelUpUI>();
            _levelUpUI.Init();
            _levelUpUI.Show(false);

            _collectionUI = GetComponentInChildren<CollectionUI>();
            //_collectionUI.Init();

        }

        public void CloseAllUI()
        {
            _introUI.SetActive(false);
            _playUI.SetActive(false);
            _gameOverUI.SetActive(false);
        }

        public void RefreshExpUI()
        {
            ExpBar expBar = GetComponentInChildren<ExpBar>();
            expBar.RefreshUI();
        }
    }
}