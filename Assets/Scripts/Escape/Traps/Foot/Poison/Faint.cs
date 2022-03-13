using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Faint : Poison
    {
        protected override void ExecutePoison()
        {
            base.ExecutePoison();
        }

        protected override void ClosePoison()
        {
            base.ClosePoison();
        }
    }
}