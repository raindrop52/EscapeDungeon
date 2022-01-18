using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum ROOM_TYPE
    {
        BASE,
        ESCAPE,
        ONLYMONSTER,
        ONLYTRAP,
        ONLYTREASURE,
        MONSTERANDTRAP,
        MONSTERANDTREASURE,
        TRAPANDTREASURE
    }

    public struct DungeonLimitCount
    {
        const int MaxMonster = 3;
        const int MaxTrap = 4;
        const int MaxTreasure = 2;
    }

    public class DungeonManager : MonoBehaviour
    {
        DungeonLimitCount _limitList;

        void Start()
        {

        }

        
        void Update()
        {

        }
    }
}