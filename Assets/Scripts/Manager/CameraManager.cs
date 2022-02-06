using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager _inst;

        public GameObject _target;
        public float _speed;
        Vector3 _targetPos;     // 타겟의 현재 위치 값


        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            MoveCam();
        }

        /* 스테이지 1 : 카메라는 상하만 캐릭터를 따라다닌다.
         * 스테이지 2 : 캐릭터는 플레이어를 따라다닌다.
         * 
         */
        
        void Update()
        {
            
        }

        void MoveCam()
        {
            StartCoroutine(_MoveCam(GameManager._inst._stageLevel));
        }

        IEnumerator _MoveCam(STAGE_LV level)
        {
            while(true)
            {
                if (_target == null)
                    break;

                _targetPos.Set(transform.position.x, _target.transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}