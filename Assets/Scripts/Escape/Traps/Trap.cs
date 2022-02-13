using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Trap_Series
    {
        NONE,
        HOLE,
        ARROW,
    }

    public class Trap : MonoBehaviour
    {
        public Trap_Series _trapSeries;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }

        void Start()
        {

        }

        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                switch(_trapSeries)
                {
                    case Trap_Series.HOLE:
                        {
                            Foot_Trap trap = this as Foot_Trap;

                            trap.OnFootTrap();

                            break;
                        }

                    case Trap_Series.ARROW:
                        {

                            break;
                        }
                }
            }
        }

        public void PlaySFX(SFX_List sfxNo)
        {
            // Æ®·¦ ¹â´Â ¼Ò¸® ÇÃ·¹ÀÌ
            SoundManager._inst.OnPlaySfx(sfxNo);
        }
    }
}