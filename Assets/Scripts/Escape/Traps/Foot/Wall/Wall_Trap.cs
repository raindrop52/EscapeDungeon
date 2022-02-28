using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Wall_Trap : Trap_Foot
    {
        [SerializeField] GameObject _hiddenTile;

        protected override void ExecuteTrap(GameObject playerObj)
        {
            OnWallTrap();
        }

        void OnWallTrap()
        {
            if (_hiddenTile != null && _hiddenTile.activeSelf == false)
            {
                // Ÿ�� ����� ����
                _hiddenTile.SetActive(true);
            }
        }
    }
}