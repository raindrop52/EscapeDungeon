using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class FxItemGet : MonoBehaviour
    {
        private void Start()
        {
            Invoke("Play", 2.0f);
        }

        public void Play()
        {
            // �÷��̾ ��ǥ��������
            Transform playerTrans = GameManager._inst._player;

            Hashtable paramHashtable = new Hashtable()
            {
                { "position", playerTrans.position},
                { "time", 1.0f },
                { "easetype", iTween.EaseType.easeInOutCubic},
                { "oncomplete", "OnComplete"},
            };

            iTween.MoveTo(gameObject, paramHashtable);
        }

        void OnComplete()
        {
            Destroy(gameObject);

            // �÷��̾��� ����ġ ����
            Player player = GameManager._inst._player.GetComponent<Player>();
            if (player != null)
            {
                player.AddExp(10);
            }
        }
    }
}