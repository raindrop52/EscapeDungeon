using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum PORTAL_DIR
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }


    public class Portal : MonoBehaviour
    {
        public PORTAL_DIR _portalDir;
        const float CameraMoveX = 1.6f;
        const float CameraMoveY = 2.3f;

        void Start()
        {

        }

        
        void Update()
        {

        }

        public int GoPortal(out float x, out float y)
        {
            int direction = 0;

            float moveX = 0.0f;
            float moveY = 0.0f;

            switch (_portalDir)
            {
                case PORTAL_DIR.LEFT:
                    {
                        moveX = CameraMoveX * -1;
                        direction = (int)PORTAL_DIR.RIGHT;
                        break;
                    }
                case PORTAL_DIR.RIGHT:
                    {
                        moveX = CameraMoveX;
                        direction = (int)PORTAL_DIR.LEFT;
                        break;
                    }
                case PORTAL_DIR.UP:
                    {
                        moveY = CameraMoveY;
                        direction = (int)PORTAL_DIR.DOWN;
                        break;
                    }
                case PORTAL_DIR.DOWN:
                    {
                        moveY = CameraMoveY * -1;
                        direction = (int)PORTAL_DIR.UP;
                        break;
                    }
                default: break;
            }

            x = moveX; y = moveY;

            CameraManager._inst.MoveCam(moveX, moveY);

            return direction;
        }
    }
}