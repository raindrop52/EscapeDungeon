using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Portal : MonoBehaviour
    {
        public Portal _moveTarget;
        public bool _moveFinshed = false;

        void Start()
        {

        }

        void Update()
        {

        }

        public void OnWarp()
        {
            _moveFinshed = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_moveTarget == null) 
                return;
            if (_moveFinshed == true) 
                return;

            if(collision.CompareTag("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Portal"))
            {
                Debug.Log("플레이어 진입");

                StartCoroutine(_DoWarp(collision));
            }
        }

        IEnumerator _DoWarp(Collider2D collision)
        {
            GameManager._inst.StartMoveStage();

            yield return new WaitForSeconds(0.5f);

            Player player = collision.transform.parent.GetComponent<Player>();
            player.ChangePlayerPos(_moveTarget.transform.position);

            // 목적지에 이동 알리기
            _moveTarget.OnWarp();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _moveFinshed = false;
            }
        }
    }
}