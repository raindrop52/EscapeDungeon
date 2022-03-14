using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class GoalUI : BaseUI
    {
        Button _btnNewGame;
        Button _btnExit;

        public override void Init()
        {
            base.Init();

            _btnNewGame = transform.Find("Btn_NewGame").GetComponent<Button>();
            if (_btnNewGame != null)
            {
                _btnNewGame.onClick.AddListener(delegate ()
                {
                    SettingStage(true);
                });
            }

            _btnExit = transform.Find("Btn_Exit").GetComponent<Button>();
            if (_btnExit != null)
            {
                _btnExit.onClick.AddListener(delegate ()
                {
                    // 저장 기록 초기화 (생각해봄)

                    // PlayUI 표시
                    UIManager._inst.NowUI = UI_ID.LOBBY;
                    UIManager._inst.ChangeUI();
                });
            }
        }
    }
}