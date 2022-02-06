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
        Vector3 _targetPos;     // Ÿ���� ���� ��ġ ��


        void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            MoveCam();
        }

        /* �������� 1 : ī�޶�� ���ϸ� ĳ���͸� ����ٴѴ�.
         * �������� 2 : ĳ���ʹ� �÷��̾ ����ٴѴ�.
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