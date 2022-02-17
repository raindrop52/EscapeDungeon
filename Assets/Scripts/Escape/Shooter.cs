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

        public void Init()
        {
            _arrow = Resources.Load("Arrow") as GameObject;
        }

        public void OnShot()
        {
            // Arrow 프리팹 생성
            GameObject arrow = Instantiate(_arrow);
            arrow.transform.position = transform.position;

            // Default 방향은 Bottom
            if (_dir == SHOOTER_DIR.RIGHT)
            {
                arrow.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else if (_dir == SHOOTER_DIR.LEFT)
            {
                arrow.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            else if (_dir == SHOOTER_DIR.TOP)
            {
                arrow.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}