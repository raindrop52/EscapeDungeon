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
        [SerializeField] Image _hpBar;

        private void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            if(_hpBar != null)
            {
                float offsetY = 1.0f;

                // 소유자의 위치를 항상 따라다니게
                Transform player = GameManager._inst._player;

                Vector3 curPos = Camera.main.WorldToScreenPoint(player.position);

                _hpBar.transform.localPosition = new Vector3(curPos.x, curPos.y - offsetY, transform.position.z);
            }
        }

        void Update()
        {

        }

        public void HPControl(float amount)
        {
            Image hpBar = _hpBar.GetComponentInChildren<Image>();

            hpBar.fillAmount = amount;
        }
    }
}