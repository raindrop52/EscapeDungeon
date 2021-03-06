using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum SHOOTER_DIR
    {
        BOTTOM,
        TOP,
        LEFT,
        RIGHT
    }

    public class Shooter : MonoBehaviour
    {
        GameObject _arrow;
        [SerializeField] SHOOTER_DIR _dir;
        public float _shotSpeed = 5.0f;

        public void Init()
        {
            _arrow = Resources.Load("Arrow") as GameObject;
        }

        public void OnShot()
        {
            // Arrow 프리팹 생성
            GameObject arrowObj = Instantiate(_arrow);
            arrowObj.transform.position = transform.position;
            arrowObj.transform.parent = GameManager._inst._arrowParent;

            // Default 방향은 Bottom
            if (_dir == SHOOTER_DIR.RIGHT)
            {
                arrowObj.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else if (_dir == SHOOTER_DIR.LEFT)
            {
                arrowObj.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            else if (_dir == SHOOTER_DIR.TOP)
            {
                arrowObj.transform.localEulerAngles = new Vector3(0, 0, 180);
            }

            Arrow arrow = arrowObj.GetComponent<Arrow>();
            arrow.Init(_shotSpeed);
        }
    }
}