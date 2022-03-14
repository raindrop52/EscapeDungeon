using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Arrow : MonoBehaviour
    {
        float _speed = 1.0f;
        float _time = 3.0f;
        AudioSource _sfx;

        public void Init(float speed)
        {
            _speed = speed;
            _sfx = GetComponentInChildren<AudioSource>();
            if (_sfx != null)
                _sfx.Play();
        }

        void FixedUpdate()
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