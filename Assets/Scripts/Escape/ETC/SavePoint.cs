using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class SavePoint : MonoBehaviour
    {
        int _layerMask;
        bool _save = false;
        float _movePlayerValue = 0.85f;

        void Start()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void OnEnable()
        {
            if(_save == false)
                StartCoroutine(_CheckInPlayer());
        }

        IEnumerator _CheckInPlayer()
        {
            RaycastHit2D hitR, hitL;
            
            while (_save == false)
            {
                // ���� ���� �߻��Ͽ� �÷��̾� üũ
                hitR = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, _layerMask);
                // ���� ���� �߻��Ͽ� �÷��̾� üũ
                hitL = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, _layerMask);
                // �¿� ���� �浹 üũ
                if (hitR || hitL)
                {
                    _save = true;

                    // ���ӸŴ����� �÷��̾� ��ġ ���� ��ġ�� ����
                    Vector3 playerPos = new Vector3(transform.position.x + _movePlayerValue, transform.position.y, transform.position.z);
                    GameManager._inst.SetSpawnPos(playerPos);
                }

                yield return null;
            }
        }
    }
}