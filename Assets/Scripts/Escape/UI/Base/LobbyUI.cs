using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class LobbyUI : BaseUI
    {
        Button _btnContinue;
        Button _btnStart;
        Button _btnOption;
        Button _btnQuit;

        public override void Init()
        {
            base.Init();

            _btnContinue = transform.Find("Btn_Continue").GetComponent<Button>();
            if (_btnContinue != null)
            {
                _btnContinue.onClick.AddListener(delegate ()
                {
                    SettingStage();
                });

                if (StageManager._inst.StageLV == 0)
                {
                    _btnContinue.gameObject.SetActive(false);
                }
            }

            _btnStart = transform.Find("Btn_Start").GetComponent<Button>();
            if (_btnStart != null)
            {
                _btnStart.onClick.AddListener(delegate ()
                {
                    if (StageManager._inst.StageLV > 0)
                    {
                        CautionUI cUi = UIManager._inst.GetUI(UI_ID.CAUTION) as CautionUI;
                        if (cUi != null)
                        {
                            cUi.OnShow(true, delegate ()
                            {
                                SettingStage(true);
                            });
                        }
                    }
                    else
                    {
                        SettingStage();
                    }
                });
            }

            _btnOption = transform.Find("Btn_Option").GetComponent<Button>();
            if (_btnOption != null)
            {
                _btnOption.onClick.AddListener(delegate ()
                {
                    // PlayUI ǥ��
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
                    //����Ƽ
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                //���ø����̼�
                Application.Quit();
#endif
                });
            }
        }

        void SettingStage(bool stageClear = false)
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }

            // PlayUI ǥ��
            UIManager._inst.NowUI = UI_ID.PLAYROOM;
            UIManager._inst.ChangeUI();

            // PlayRoomUI �ʱ�ȭ(���� ����)
            // �� �Է��� ���� ��� �������� ����� ���������� ����
            if(stageClear == true)
                StageManager._inst.SetStageLV(Stage_LV.RESTROOM);
            // �������� �ʱ�ȭ
            StageManager._inst.StageInit();

            GameManager._inst.PlayInit();
        }
    }
}