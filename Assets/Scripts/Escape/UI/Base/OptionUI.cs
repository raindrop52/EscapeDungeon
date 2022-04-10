using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class OptionUI : BaseUI
    {
        Button _btnBack;

        public override void Init()
        {
            base.Init();

            _btnBack = transform.Find("Btn_Back").GetComponent<Button>();
            if (_btnBack != null)
            {
                _btnBack.onClick.AddListener(delegate ()
                {
                    // PlayUI Ç¥½Ã
                    UIManager._inst.NowUI = UI_ID.LOBBY;
                    UIManager._inst.ChangeUI();
                });
            }
        }
    }
}