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
                // �̸� üũ (Ȥ�� �� ������ġ)
                if(collision.gameObject.name == "Player")
                {
                    // �÷��̾� ���� ������
                    Player player = collision.GetComponent<Player>();

                    // �÷��̾� ��Ʈ�� ������
                    Player_Control pc = player.GetComponent<Player_Control>();
                    // �÷��̾� ��Ʈ�� false ( ���� �Ұ� )
                    pc.enabled = false;

                    MessageBox msgBox = UIManager._inst._messageBox;
                    // �ؽ�Ʈ �޽��� �ڽ� ǥ��
                    if(msgBox != null)
                    {
                        msgBox.Init(gameObject,_message);

                        // �޽��� �ڽ� ���̱�
                        msgBox.OnShow(true);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // �̸� üũ (Ȥ�� �� ������ġ)
                if (collision.gameObject.name == "Player")
                {
                    // �÷��̾� ���� ������
                    Player player = collision.GetComponent<Player>();

                    // �÷��̾� ��Ʈ�� ������
                    Player_Control pc = player.GetComponent<Player_Control>();
                    // �÷��̾� ��Ʈ�� true
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