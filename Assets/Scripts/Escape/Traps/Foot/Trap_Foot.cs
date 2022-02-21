using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Trap_Foot : Trap
    {
        protected virtual void ExcuteTrap()
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                ExcuteTrap();
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            
        }
    }
}