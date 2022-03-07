using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class BaseUI : MonoBehaviour
    {
        public virtual void Init()
        {

        }

        public void OnShow(bool show)
        {
            if (gameObject.activeSelf != show)
            {
                gameObject.SetActive(show);
            }
        }

        protected virtual void OnEnable()
        {
            // Ȱ��ȭ ��
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
        }
    }
}