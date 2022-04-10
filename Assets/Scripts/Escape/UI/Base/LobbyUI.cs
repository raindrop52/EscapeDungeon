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

        protected override void OnEnable()
        {
            int lv = StageManager._inst.StageLV;

            if (_btnContinue != null)
            {
                if (lv == 0)
                {
                    _btnContinue.gameObject.SetActive(false);
                }
                else
                {
                    _btnContinue.gameObject.SetActive(true);
                }
            }

            base.OnEnable();
        }

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
                    // TODO : OptionUI 표시
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