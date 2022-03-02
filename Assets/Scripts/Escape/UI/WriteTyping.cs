using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class WriteTyping : MonoBehaviour
    {
        Text m_TypingText;
        Callback _cb;

        public string m_Message;
        public float m_Speed = 0.2f;

        public void Init(string text)
        {
            if(m_TypingText == null)
                m_TypingText = GetComponentInChildren<Text>();

            m_Message = text;
            m_TypingText.text = text;
        }

        public void Init(Callback cb)
        {
            _cb = cb;

            m_TypingText = GetComponentInChildren<Text>();

            Debug.Log("대화창 진입");

            if(m_TypingText != null)
                StartCoroutine(Typing(m_TypingText, m_Message, m_Speed));
        }

        IEnumerator Typing(Text typingText, string message, float speed)
        {
            string text = message.Replace("\\n", "\n");
            string temp;

            for (int i = 0; i < text.Length; i++)
            {
                temp = text.Substring(0, i + 1);

                typingText.text = temp;
                
                yield return new WaitForSeconds(speed);
            }

            yield return new WaitForSeconds(1.0f);

            if (_cb != null)
            {
                _cb();
            }
        }

        //float time;
        //private void FixedUpdate()
        //{
        //    string text = m_Message.Replace("\\n", "\n");
        //    string temp;
        //    int i = 0;
        //    time = 0f;

        //    while(i < text.Length)
        //    {                
        //        if(time < m_Speed)
        //        {
        //            time += Time.fixedUnscaledTime;
        //            continue;
        //        }

        //        temp = text.Substring(0, i + 1);

        //        m_TypingText.text = temp;
        //        i++;
        //        time = 0f;
        //    }

        //    time = 0f;
        //    while(time < 1.0f)
        //    {
        //        time += Time.fixedUnscaledTime;
        //    }

        //    if (_cb != null)
        //    {
        //        _cb();
        //    }
        //}
    }
}