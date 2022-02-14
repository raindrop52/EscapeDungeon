using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Talk_Read : TalkTrigger
    {
        protected override void DoTrigerEvent()
        {
            switch (_talkInfo._order)
            {
                case 0:
                    {
                        UIManager._inst.ShowTextMessage(_talkInfo._text);
                        break;
                    }
                default:
                    break;
            }

            base.DoTrigerEvent();
        }
    }
}