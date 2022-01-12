using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        public List<GameObject> _doorList;
        public List<GameObject> _arrowList;

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {

        }


        void Update()
        {

        }
    }
}