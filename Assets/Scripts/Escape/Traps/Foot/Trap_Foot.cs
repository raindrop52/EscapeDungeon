using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Trap_Foot : Trap
    {
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            base.OnDrawGizmos();
        }

        protected virtual void ExecuteTrap(GameObject playerObj)
        {

        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                ExecuteTrap(collision.gameObject);
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