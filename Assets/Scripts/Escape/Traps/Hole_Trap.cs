using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Hole_Trap : Trap
    {
        public GameObject _hiddenTile;



        public void OnHoleTrap()
        {
            if (_hiddenTile != null && _hiddenTile.activeSelf == false)
            {
                PlaySFX(SFX_List.HOLE);

                // Ʈ�� ����
                _hiddenTile.SetActive(true);
            }
        }
    }
}