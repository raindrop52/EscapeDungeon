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

            // �浹ü
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
                        // ���� ����Ʈ ǥ��
                        if(_psBomb != null)
                        {
                            _psBomb.Play();
                        }

                        // ������ Ÿ�� ǥ��
                        StageManager._inst.ShowHiddenTile(true);

                        // 2�� �� �����
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