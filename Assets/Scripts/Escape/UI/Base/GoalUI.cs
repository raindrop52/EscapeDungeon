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
                    if (Time.timeScale != 1)
                    {
                        Time.timeScale = 1;
                    }

                    // PlayRoomUI ǥ��
                    UIManager._inst.NowUI = UI_ID.PLAYROOM;
                    UIManager._inst.ChangeUI();

                    // �������� �ʱ�ȭ
                    StageManager._inst.StageInit();

                    // PlayRoomUI �ʱ�ȭ(���� ����)
                    GameManager._inst.PlayInit();
                });
            }

            _btnExit = transform.Find("Btn_Exit").GetComponent<Button>();
            if (_btnExit != null)
            {
                _btnExit.onClick.AddListener(delegate ()
                {
                    // PlayUI ǥ��
                    UIManager._inst.NowUI = UI_ID.LOBBY;
                    UIManager._inst.ChangeUI();
                });
            }
        }
    }
}