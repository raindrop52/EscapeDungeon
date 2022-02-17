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
            StartCoroutine(_Shot());
        }

        IEnumerator _Shot()
        {
            // Arrow 橇府普 积己
            GameObject arrow = Instantiate(_arrow);
            arrow.transform.position = transform.position;

            // Default 规氢篮 Bottom
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

            yield return null;
        }
    }
}