using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Foot_Trap : Trap
    {
        public GameObject _hiddenTile;

        public void OnFootTrap()
        {
            if (_hiddenTile != null && _hiddenTile.activeSelf == false)
            {
                PlaySFX(SFX_List.HOLE);

                // ∆Æ∑¶ µø¿€
                _hiddenTile.SetActive(true);
            }
        }
    }
}