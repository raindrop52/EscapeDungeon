using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class MessageBox : BaseUI
    {
        Text _messageBox = null;
        GameObject _owner = null;

        public void Init(GameObject owner, string content = "")
        {
            _owner = owner;

            if(_messageBox == null)
                _messageBox = GetComponentInChildren<Text>();

            if(_messageBox != null)
            {
                _messageBox.text = content;
            }
        }

        private void FixedUpdate()
        {
            CloseMessageBox();
        }

        void CloseMessageBox()
        {
            Player player = GameManager._inst._player;

            if (player._useTutorial == false) return;

            if (Input.GetKeyDown(KeyCode.Space) == true || player._btnDo == true)
            {
                if (player._btnDo == true)
                    player._btnDo = false;

                OnShow(false);

                MessageEventTrigger trigger = _owner.GetComponent<MessageEventTrigger>();
                
                if(trigger != null)
                {
                    trigger.DisableMessage();
                }
            }
        }
    }
}