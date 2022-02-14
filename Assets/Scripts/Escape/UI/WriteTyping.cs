using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class WriteTyping : MonoBehaviour
    {
        Text m_TypingText;

        public string m_Message;
        public float m_Speed = 0.2f;

        public void Init()
        {
            m_TypingText = GetComponentInChildren<Text>();

            if(m_TypingText != null)
                StartCoroutine(Typing(m_TypingText, m_Message, m_Speed));
        }

        IEnumerator Typing(Text typingText, string message, float speed)
        {
            string text = message.Replace("\\n", "\n");
            string temp = "";
            string tmp1, tmp2;

            for (int i = 0; i < text.Length; i++)
            {
                temp = text.Substring(0, i + 1);
                tmp1 = text.Substring(i, 1);
                //if ()

                typingText.text = temp;
                
                yield return new WaitForSeconds(speed);
            }
        }
    }
}