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
        bool _moveTarget = false;
        public bool MoveTarget
        { get { return _moveTarget; } set { _moveTarget = value; } }

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            MoveCam();
        }
                
        void Update()
        {
            
        }

        void MoveCam()
        {
            StartCoroutine(_MoveCam());
        }

        IEnumerator _MoveCam()
        {
            while(true)
            {
                if (_target == null)
                    break;

                _targetPos.Set(_target.transform.position.x, _target.transform.position.y, transform.position.z);

                if(_moveTarget == true)
                    transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
                else
                    transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}