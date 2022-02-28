using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Talk_Break : Trap_Talk
    {
        public ParticleSystem _psBomb;

        protected override void Start()
        {
            base.Start();

            Transform bombTrans = transform.Find("Bomb");
            if (bombTrans != null)
            {
                _psBomb = bombTrans.GetComponent<ParticleSystem>();
            }
        }

        protected override void DoTrigerEvent()
        {
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

                        gameObject.SetActive(false);
                        break;
                    }
                default :
                    break;
            }

            base.DoTrigerEvent();
        }
    }
}