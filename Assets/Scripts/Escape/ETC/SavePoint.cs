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
                // 우측 레이 발사하여 플레이어 체크
                hitR = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, _layerMask);
                // 좌측 레이 발사하여 플레이어 체크
                hitL = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, _layerMask);
                // 좌우 레이 충돌 체크
                if (hitR || hitL)
                {
                    _save = true;

                    // 게임매니저의 플레이어 위치 현재 위치로 설정
                    Vector3 playerPos = new Vector3(transform.position.x + _movePlayerValue, transform.position.y, transform.position.z);
                    GameManager._inst.SetSpawnPos(playerPos);
                }

                yield return null;
            }
        }
    }
}