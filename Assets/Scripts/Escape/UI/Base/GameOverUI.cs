using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class GameOverUI : BaseUI
    {
        Transform _moveTrans;
        Button _btnContinue;
        Button _btnExit;

        public override void Init()
        {
            base.Init();

            _moveTrans = transform.Find("MoveControl");
            if(_moveTrans != null)
            {
                _btnContinue = _moveTrans.Find("Btn_Continue").GetComponent<Button>();
                if (_btnContinue != null)
                {
                    _btnContinue.onClick.AddListener(delegate ()
                    {
                        if (Time.timeScale != 1)
                        {
                            Time.timeScale = 1;
                        }

                        // PlayRoomUI 표시
                        UIManager._inst.NowUI = UI_ID.PLAYROOM;
                        UIManager._inst.ChangeUI();

                        // 스테이지 초기화
                        StageManager._inst.StageInit();

                        // PlayRoomUI 초기화(동작 관련)
                        GameManager._inst.PlayInit();
                    });
                }

                _btnExit = _moveTrans.Find("Btn_Exit").GetComponent<Button>();
                if (_btnExit != null)
                {
                    _btnExit.onClick.AddListener(delegate ()
                    {
                        // PlayUI 표시
                        UIManager._inst.NowUI = UI_ID.LOBBY;
                        UIManager._inst.ChangeUI();
                    });
                }
            }
        }
    }
}