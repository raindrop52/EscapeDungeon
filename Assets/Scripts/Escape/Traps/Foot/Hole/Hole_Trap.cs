using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Hole_Trap : Trap_Foot
    {
        [SerializeField] GameObject _hiddenTile;
        [SerializeField] float _showTime = 0.0f;       // ���� �ð�

        protected override void ExecuteTrap(GameObject playerObj)
        {
            StartCoroutine(_OnHoleTrap());
        }

        IEnumerator _OnHoleTrap()
        {
            // ���� �ð� ��� �� Ʈ�� ����
            yield return new WaitForSeconds(_showTime);

            OnHoleTrap();
        }

        void OnHoleTrap()
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