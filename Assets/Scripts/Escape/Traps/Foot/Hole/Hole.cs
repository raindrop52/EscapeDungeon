using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Hole : MonoBehaviour
    {
        public float _fallTime;         //�������� �ð�

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("�÷��̾� ����");

                Player_Control pc = collision.transform.GetComponent<Player_Control>();
                if (pc != null)
                    pc.enabled = false;

                // �÷��̾� �߶� �̺�Ʈ ����
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

                yield return new WaitForSeconds(0.05f);
            }

            Player_Control pc = playerGo.GetComponent<Player_Control>();
            if (pc != null)
                pc.enabled = true;

            GameManager._inst.Die = true;
        }
    }
}