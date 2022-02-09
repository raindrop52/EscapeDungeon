using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player : MonoBehaviour
    {
        [Header("캐릭터 피격 관련")]
        bool _hit = false;
        public bool HitStatus
        {
            get
            {
                return _hit;
            }
        }
        float _hitCoolTime = 1.0f;

        public bool _dashDodge = false;     // 무적 회피 처리

        void Start()
        {
        }

        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Attack")
            {
                if (_dashDodge)
                {
                    return;
                }

                if (!_hit)
                {
                    _hit = true;
                    StartCoroutine(_HitEffect());
                }
            }

            if (collision.tag == "Portal")
            {
                GameManager._inst.StartMoveStage();
            }
        }

        IEnumerator _HitEffect()
        {
            Animator anim = GetComponent<Animator>();

            anim.SetTrigger("Hit");

            yield return new WaitForSeconds(_hitCoolTime);

            _hit = false;
        }


        // 캐릭터 이동 시 위치 변경
        public void ChangePlayerPos(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}