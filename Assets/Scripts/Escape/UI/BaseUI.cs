using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class BaseUI : MonoBehaviour
    {
        void Start()
        {

        }


        void Update()
        {

        }

        public void OnShow(bool show)
        {
            if (gameObject.activeSelf != show)
            {
                gameObject.SetActive(show);
            }
        }
    }
}