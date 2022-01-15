using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager _inst;

        

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

        public void MoveCam(float x, float y)
        {
            Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            
            transform.position = pos;
        }
    }
}