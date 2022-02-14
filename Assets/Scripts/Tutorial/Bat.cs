using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Bat : MapObject
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Attack")
            {
                return;
            }
            
            OnHit(collision);
        }
    }
}