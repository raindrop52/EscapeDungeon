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

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            ExpBar expBar = GetComponentInChildren<ExpBar>();
            expBar.Init();
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