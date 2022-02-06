using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Arrow : MonoBehaviour
    {
        public float _speed = 1.0f;
        float _time = 20.0f;

        void Start()
        {

        }

        
        void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);

            Invoke("Disappear", _time);
        }

        void Disappear()
        {
            Destroy(gameObject);
        }
    }
}