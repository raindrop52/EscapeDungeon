using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class MessageEventTrigger : MonoBehaviour
    {
        BoxCollider2D _boxCol;
        ParticleSystem _effectNotice;
        [SerializeField] string _message;

        void Start()
        {
            _boxCol = GetComponent<BoxCollider2D>();
            _effectNotice = GetComponentInChildren<ParticleSystem>();

            Player player = GameManager._inst._player;
            if(player._useTutorial == false)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                // 이름 체크 (혹시 모를 안전장치)
                if(collision.gameObject.name == "Player")
                {
                    // 플레이어 정보 가져옴
                    Player player = collision.GetComponent<Player>();

                    // 플레이어 컨트롤 가져옴
                    Player_Control pc = player.GetComponent<Player_Control>();
                    // 플레이어 컨트롤 false ( 동작 불가 )
                    pc.enabled = false;

                    MessageBox msgBox = UIManager._inst._messageBox;
                    // 텍스트 메시지 박스 표시
                    if(msgBox != null)
                    {
                        msgBox.Init(gameObject,_message);

                        // 메시지 박스 보이기
                        msgBox.OnShow(true);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // 이름 체크 (혹시 모를 안전장치)
                if (collision.gameObject.name == "Player")
                {
                    // 플레이어 정보 가져옴
                    Player player = collision.GetComponent<Player>();

                    // 플레이어 컨트롤 가져옴
                    Player_Control pc = player.GetComponent<Player_Control>();
                    // 플레이어 컨트롤 true
                    pc.enabled = true;
                }
            }
        }

        public void DisableMessage()
        {
            if (_effectNotice != null)
                _effectNotice.gameObject.SetActive(false);

            _boxCol.enabled = false;
        }
    }
}