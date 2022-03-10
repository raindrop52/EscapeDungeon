using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class RestRoom : Stage_Base
    {
        public override void Init()
        {
            Vector3 pos = new Vector3(_savePoint.position.x + 0.85f, _savePoint.position.y, 0f);
            GameManager._inst._player.ChangePlayerPos(pos);
            

            base.Init();
        }
    }
}