using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class MessageBox : BaseUI
    {
        Text _messageBox;

        public void Init(string content = "")
        {
            _messageBox = GetComponentInChildren<Text>();

            if(_messageBox != null)
            {
                _messageBox.text = content;
            }
        }
    }
}