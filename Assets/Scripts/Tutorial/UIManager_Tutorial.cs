using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Update is called once per frame
        void Update()
        {

        }
    }
}