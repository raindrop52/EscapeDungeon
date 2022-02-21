using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Hole_Trap : Trap_Foot
    {
        [SerializeField] GameObject _hiddenTile;

        protected override void ExcuteTrap()
        {
            OnHoleTrap();
        }

        void OnHoleTrap()
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