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

        void Start()
        {
            
        }

        void Update()
        {

        }

    }
}