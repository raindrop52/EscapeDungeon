using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Bleeding : Poison
    {
        protected override void ExecutePoison(Player player)
        {
            base.ExecutePoison(player);
        }

        protected override void ClosePoison()
        {
            base.ClosePoison();
        }
    }
}