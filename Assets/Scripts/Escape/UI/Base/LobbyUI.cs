using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class LobbyUI : BaseUI
    {
        Button _btnStart;
        Button _btnOption;
        Button _btnQuit;

        public override void Init()
        {
            base.Init();

            _btnStart = transform.Find("Btn_Start").GetComponent<Button>();
            if (_btnStart != null)
            {
                _btnStart.onClick.AddListener(delegate ()
                {
                    if (Time.timeScale != 1)
                    {
                        Time.timeScale = 1;
                    }

                    // PlayUI 표시
                    UIManager._inst.NowUI = UI_ID.PLAYROOM;
                    UIManager._inst.ChangeUI();

                    // PlayRoomUI 초기화(동작 관련)
                    GameManager._inst.PlayInit();
                });
            }

            _btnOption = transform.Find("Btn_Option").GetComponent<Button>();
            if (_btnOption != null)
            {
                _btnOption.onClick.AddListener(delegate ()
                {
                    // PlayUI 표시
                    UIManager._inst.NowUI = UI_ID.OPTION;
                    UIManager._inst.ChangeUI();
                });
            }

            _btnQuit = transform.Find("Btn_Quit").GetComponent<Button>();
            if (_btnQuit != null)
            {
                _btnQuit.onClick.AddListener(delegate ()
                {
#if UNITY_EDITOR
                    //유니티
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                //어플리케이션
                Application.Quit();
#endif
                });
            }
        }
    }
}