using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Wall_Trap : Trap_Foot
    {
        [SerializeField] List<GameObject> _hiddenTiles;

        protected override void ExecuteTrap(GameObject playerObj)
        {
            OnWallTrap();
        }

        void OnWallTrap()
        {
            if (_hiddenTiles.Count > 0)
            {
                foreach(GameObject go in _hiddenTiles)
                {
                    // Ÿ�� ����� ����
                    go.SetActive(true);
                }
            }
        }
    }
}