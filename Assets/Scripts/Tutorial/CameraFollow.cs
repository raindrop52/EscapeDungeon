using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform _target;

        void Start()
        {

        }

        
        void Update()
        {
            TutorialCam();
        }

        void TutorialCam()
        {
            if (_target != null)
            {
                Vector3 pos = _target.position;

                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
        }
    }
}