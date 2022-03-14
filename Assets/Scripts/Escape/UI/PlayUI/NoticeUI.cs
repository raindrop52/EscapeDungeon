using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class NoticeUI : MonoBehaviour
    {
        [SerializeField] Transform _keyTrans;

        public void Init()
        {
            if(_keyTrans != null)
            {
                Tutorial_Key[] keys = GetComponentsInChildren<Tutorial_Key>();
                foreach(Tutorial_Key key in keys)
                {
                    key.Init();
                }
            }
        }
    }
}