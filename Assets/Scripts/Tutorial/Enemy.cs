using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Enemy : MapObject
    {
        public GameObject _player;
        public float _speed;

        protected override void Start()
        {
            base.Start();

            _player = GameManager._inst._playerTrans.gameObject;

            StartCoroutine(_TraceTarget());
        }

        IEnumerator _TraceTarget()
        {
            while (_player != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
                
                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Attack")
            {
                return;
            }

            OnHit(collision);
        }
    }
}