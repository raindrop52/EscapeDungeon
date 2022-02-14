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
            // 플레이어를 목표지점으로
            Transform playerTrans = GameManager._inst._player;

            iTween.MoveTo(gameObject, playerTrans.position, 1.0f);

            //Hashtable paramHashtable = new Hashtable();            
        }
    }
}