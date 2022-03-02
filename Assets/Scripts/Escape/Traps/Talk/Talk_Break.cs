using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Talk_Break : Trap_Talk
    {
        public ParticleSystem _psBomb;
        public BoxCollider2D _col;

        protected override void Start()
        {
            base.Start();

            // 충돌체
            _col = GetComponent<BoxCollider2D>();

            Transform bombTrans = transform.Find("Bomb");
            if (bombTrans != null)
            {
                _psBomb = bombTrans.GetComponent<ParticleSystem>();
            }
        }

        protected override void DoTrigerEvent()
        {
            if(_col != null)
            {
                _col.enabled = false;
            }

            switch(_talkInfo._order)
            {
                case 0 :
                    {
                        // 폭발 이펙트 표시
                        if(_psBomb != null)
                        {
                            _psBomb.Play();
                        }

                        // 숨겨진 타일 표시
                        StageManager._inst.ShowHiddenTile(true);

                        // 2초 후 숨기기
                        Invoke("OnHide", 2.0f);

                        break;
                    }
                default :
                    break;
            }

            base.DoTrigerEvent();
        }

        public void OnHide()
        {
            gameObject.SetActive(false);
        }
    }
}