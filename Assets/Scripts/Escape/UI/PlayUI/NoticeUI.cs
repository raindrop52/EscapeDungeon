using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class NoticeUI : MonoBehaviour
    {
        [SerializeField] Transform _keyTrans;
        public Notice_Tutorial _notice_Tutorial;

        public void Init()
        {
            if(_keyTrans != null)
            {
                Tutorial_Key[] keys = GetComponentsInChildren<Tutorial_Key>();
                foreach (Tutorial_Key key in keys)
                {
                    key.Init();
                }
            }

            _notice_Tutorial = transform.Find("Tutorial_Mobile").GetComponent<Notice_Tutorial>();
            if(_notice_Tutorial != null)
            {
                _notice_Tutorial.Init();
            }
        }
    }
}