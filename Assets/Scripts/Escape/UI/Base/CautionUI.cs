using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class CautionUI : BaseUI
    {
        Button _btnYes;
        Button _btnNo;
        Callback _cb = null;

        public override void Init()
        {
            base.Init();

            _btnYes = transform.Find("Btn_Yes").GetComponent<Button>();
            if (_btnYes != null)
            {
                _btnYes.onClick.AddListener(delegate ()
                {
                    OnShow(false);

                    if (_cb != null)
                    {
                        _cb();
                        _cb = null;
                    }
                });
            }

            _btnNo = transform.Find("Btn_No").GetComponent<Button>();
            if (_btnNo != null)
            {
                _btnNo.onClick.AddListener(delegate ()
                {
                    OnShow(false);

                    if (_cb != null)
                    {
                        _cb = null;
                    }
                });
            }
        }
        
        public void OnShow(bool show, Callback cb)
        {
            if (_cb == null)
                _cb = cb;

            gameObject.SetActive(show);
        }
    }
}