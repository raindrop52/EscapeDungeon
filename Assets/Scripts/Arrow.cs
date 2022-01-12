using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum DIR_Type
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }

    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        DIR_Type _arrowType;
        Animator _anim;

        void Start()
        {
            _anim = GetComponent<Animator>();

            if(_anim != null)
                ArrowAnimSet();
        }

        void Update()
        {

        }

        void ArrowAnimSet()
        {
            switch (_arrowType)
            {
                case DIR_Type.LEFT:
                    {
                        _anim.SetBool("Left", true);
                        break;
                    }
                case DIR_Type.RIGHT:
                    {
                        _anim.SetBool("Right", true);
                        break;
                    }
                case DIR_Type.UP:
                    {
                        _anim.SetBool("Up", true);
                        break;
                    }
                case DIR_Type.DOWN:
                    {
                        _anim.SetBool("Down", true);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}