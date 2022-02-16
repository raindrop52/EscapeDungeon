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

        public LevelUpUI _levelUpUI;
        public CollectionUI _collectionUI;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            Transform playUI = transform.Find("PlayUI");

            ExpBar expBar = playUI.GetComponentInChildren<ExpBar>();
            expBar.Init();

            _levelUpUI = playUI.GetComponentInChildren<LevelUpUI>();
            _levelUpUI.Init();
            _levelUpUI.Show(false);

            //_collectionUI = GetComponentInChildren<CollectionUI>();
            //_collectionUI.Init();
        }

        void Update()
        {

        }

        public void RefreshExpUI()
        {
            ExpBar expBar = GetComponentInChildren<ExpBar>();
            expBar.RefreshUI();
        }
    }
}