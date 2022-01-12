using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Portal : MonoBehaviour
    {
        
        void Start()
        {

        }

        
        void Update()
        {

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Portal")
            {
                Debug.Log(transform.name + " Æ÷Å» ºÎµúÈû");
            }
        }
    }
}