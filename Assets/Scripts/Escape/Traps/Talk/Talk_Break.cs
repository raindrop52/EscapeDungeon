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
        }

        protected override void DoTriggerEvent()
        {
            int order = _talkInfo._order;
            switch (order)
            {
                case 0 :
                    {
                        // Æø¹ß ÀÌÆåÆ® Ç¥½Ã
                        if(_psBomb != null)
                        {
                            PlaySFX(SFX_List.BOMB);
                            _psBomb.Play();
                        }

                        // ¼û°ÜÁø Å¸ÀÏ Ç¥½Ã
                        StageManager._inst.DoStageEvent(order);

                        // 2ÃÊ ÈÄ ¼û±â±â
                        Invoke("OnHide", 0.5f);

                        break;
                    }
                default :
                    break;
            }

            base.DoTriggerEvent();
        }

        public void OnShow()
        {
            gameObject.SetActive(true);
        }

        public void OnHide()
        {
            gameObject.SetActive(false);
        }
    }
}