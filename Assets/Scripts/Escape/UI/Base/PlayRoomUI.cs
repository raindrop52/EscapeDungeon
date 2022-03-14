using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class PlayRoomUI : BaseUI
    {
        GameObject _textReadPanel;

        StatusUI _statusUI;
        ControlUI _controlUI;
        MessageBox _messageBox;

        bool _talking = false;
        public bool Talking
        { get { return _talking; } }

        protected override void OnEnable()
        {
            // 동작 X
        }

        public override void Init()
        {
            base.Init();

            _textReadPanel = GetComponentInChildren<WriteTyping>(true).gameObject;
            if (_textReadPanel.activeSelf)
            {
                // 초기화 시 활성화 상태인 경우 비활성화 처리
                ShowTextPanel(false);
            }

            _messageBox = GetComponentInChildren<MessageBox>(true);
            if (_messageBox != null)
            {
                _messageBox.OnShow(false);
            }

            _statusUI = transform.Find("StatusUI").GetComponentInChildren<StatusUI>();
            _statusUI.Init();
            _controlUI = transform.Find("ControlUI").GetComponentInChildren<ControlUI>();
            _controlUI.Init();
        }

        public void StageInit()
        {
            // 플레이어 체력바 초기화

            // 플레이어 디버프(중독) 상태 초기화

        }

        public bool IsTextPanel()
        {
            return _textReadPanel.activeSelf;
        }

        void ShowTextPanel(bool show)
        {
            _textReadPanel.SetActive(show);
        }

        // UI에 지정 텍스트 표시
        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            // 강제 숨김
            if (forceHide)
            {
                if (_talking == true)
                    _talking = false;

                ShowTextPanel(false);
            }
            else
            {
                if (_textReadPanel != null)
                {
                    // show = 텍스트 창 표시 상태
                    bool isShow = _textReadPanel.activeSelf;

                    // UI 텍스트 창 활성화(꺼져있는 상태)
                    if (isShow == false)
                    {
                        _talking = true;

                        ShowTextPanel(true);

                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init(delegate ()
                        {
                            // 텍스트 메시지 창 닫기
                            _talking = false;
                            ShowTextPanel(false);
                        });
                    }
                }
            }
        }

        public MessageBox GetMessageBox()
        {
            return _messageBox;
        }

        public void OnHit()
        {
            _statusUI.OnHitUI();
        }

        public void OnHeal()
        {
            _statusUI.OnHealUI();
        }

        public void OnPoisoning(Poison_Type type, float holdingTime)
        {
            _statusUI.OnPoisoningUI(type, holdingTime);
        }

        public void OnPoisoningTimeReset()
        {
            _statusUI.OnPoisonUIResetTime();
        }

        public void OnClearPoison()
        {
            _statusUI.OnClearPoisonUI();
        }
    }
}