using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Player_StateBase : MonoBehaviour
    {
        protected Player_StateManager _stateMgr;

        public virtual void Init(Player_StateManager stateMgr)
        {
            _stateMgr = stateMgr;
        }

        public virtual void OnEnter()
        {
            enabled = true;

        }

        public virtual void OnExit()
        {

            enabled = false;
        }

        private void Update()
        {
            
        }
    }
}