using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    //public enum Trap_Series
    //{
    //    NONE,
    //    HOLE,
    //    ARROW,
    //}

    public class Trap : MonoBehaviour
    {
        //public Trap_Series _trapSeries;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                if (this is Hole_Trap)
                {
                    Hole_Trap trap = this as Hole_Trap;

                    trap.OnHoleTrap();
                }
                else if (this is Arrow_Trap)
                {
                    Arrow_Trap trap = this as Arrow_Trap;

                    trap.Init();

                    if (trap.IsShot == false)
                    {
                        trap.IsShot = true;
                    }
                    trap.OnShot();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (this is Hole_Trap)
                {
                    Hole_Trap trap = this as Hole_Trap;

                }
                else if (this is Arrow_Trap)
                {
                    Arrow_Trap trap = this as Arrow_Trap;

                    if(trap.IsShooting == false)
                    {
                        trap.IsShooting = true;
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (this is Hole_Trap)
                {
                    Hole_Trap trap = this as Hole_Trap;

                }
                else if (this is Arrow_Trap)
                {
                    Arrow_Trap trap = this as Arrow_Trap;

                    if (trap.IsShooting == true)
                    {
                        trap.IsShooting = false;
                    }

                    if (trap.IsShot == true)
                    {
                        trap.IsShot = false;
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