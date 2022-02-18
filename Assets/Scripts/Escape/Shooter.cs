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
            // Arrow 橇府普 积己
            GameObject arrowObj = Instantiate(_arrow);
            arrowObj.transform.position = transform.position;

            // Default 规氢篮 Bottom
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
            arrow._speed = _shotSpeed;
        }
    }
}