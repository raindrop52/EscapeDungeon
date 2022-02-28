using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Wall : MonoBehaviour
    {
        public float _waitTime = 0.5f;
        public float _cooltime = 0.1f;
        public float _playTime = 0.0f;
        public float _speed = 1.0f;
        public float _moveMeter = 4.5f;

        private void OnEnable()
        {
            if(gameObject.activeSelf == true)
            {
                Init();
            }
            else
            {
                transform.position = Vector3.zero;
            }
        }

        void Init()
        {
            _playTime = 0.0f;
            StartCoroutine(_WallMoveStart());
        }

        IEnumerator _WallMoveStart()
        {
            while(gameObject.activeSelf)
            {
                _playTime += Time.fixedDeltaTime;

                Vector3 pos = new Vector3(Mathf.PingPong(_playTime * _speed, _moveMeter), transform.position.y, transform.position.z);
                
                transform.position = pos;

                yield return new WaitForSeconds(_cooltime);

                if (transform.position.x <= 0.01f)
                    yield return new WaitForSeconds(_waitTime);
            }
        }
    }
}