using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    interface IRandom
    {
        bool CheckRandomEvent(int num, int per);
        bool CheckRandomEvent(int num, int minPer, int maxPer);
    }
}
