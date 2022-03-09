using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Hole_Trap : Trap_Foot
    {
        [SerializeField] GameObject _hiddenTile;
        [SerializeField] float _showTime = 0.0f;       // 등장 시간

        protected override void ExecuteTrap(GameObject playerObj)
        {
            StartCoroutine(_OnHoleTrap());
        }

        IEnumerator _OnHoleTrap()
        {
            // 트랩 사운드 동작
            PlaySFX(SFX_List.HOLE);

            // 일정 시간 대기 후 트랩 동작
            yield return new WaitForSeconds(_showTime);

            OnHoleTrap();
        }

        void OnHoleTrap()
        {
            if (_hiddenTile != null && _hiddenTile.activeSelf == false)
            {
                // 트랩 동작
                _hiddenTile.SetActive(true);
            }
        }
    }
}