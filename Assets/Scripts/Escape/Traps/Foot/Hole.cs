using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Hole : MonoBehaviour
    {
        public float _fallTime;         //떨어지는 시간

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("플레이어 진입");

                Player_Control pc = collision.transform.GetComponent<Player_Control>();
                if (pc != null)
                    pc.enabled = false;
                
                // 플레이어 추락 이벤트 동작
                StartCoroutine(_FallPlayer(collision.gameObject));                
            }
        }

        IEnumerator _FallPlayer(GameObject playerGo)
        {
            float time = 0.0f;

            while (time < _fallTime)
            {
                time += Time.fixedUnscaledDeltaTime;

                playerGo.transform.localScale = Vector3.Lerp(playerGo.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), time / _fallTime);

                yield return null;
            }
        }

        void FallPlayer(GameObject playerGo)
        {
            float time = 0.0f;
            float elepseadTime = 1f;
            int i = 0;

            while(playerGo.transform.localScale != new Vector3(1.0f, 1.0f, 1.0f))
            {
                time += Time.fixedUnscaledDeltaTime;

                if(time >= elepseadTime)
                {
                    Debug.Log(i++);
                    time = 0.0f;
                    playerGo.transform.localScale = Vector3.Lerp(playerGo.transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.fixedUnscaledDeltaTime);
                }
            }
        }
    }
}