using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Life : MonoBehaviour
    {
        public void ShowLife(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}