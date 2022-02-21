using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Talk_Trap : TalkTrigger
    {
        protected override void DoTrigerEvent()
        {
            switch (_talkInfo._order)
            {
                case 0:
                    {
                        AIManager._inst.ShowHiddenTile(true);
                        break;
                    }
                default:
                    break;
            }

            base.DoTrigerEvent();
        }
    }
}