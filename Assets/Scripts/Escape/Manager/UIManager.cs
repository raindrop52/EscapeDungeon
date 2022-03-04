using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _inst;

        GameObject _textReadPanel;

        #region UI
        public StatusUI _statusUI;
        public ControlUI _controlUI;
        public MessageBox _messageBox;
        #endregion

        bool _talking = false;
        public bool Talking
        { get { return _talking; } }

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            _textReadPanel = GetComponentInChildren<WriteTyping>(true).gameObject;
            if(_textReadPanel.activeSelf)
            {
                // 초기화 시 활성화 상태인 경우 비활성화 처리
                ShowTextPanel(false);
            }

            if(_messageBox != null)
            {
                _messageBox.OnShow(false);
            }

            // UI 초기화
            // 로비 UI 초기화

            // 플레이 UI 초기화
            _statusUI = transform.Find("StatusUI").GetComponentInChildren<StatusUI>();
            _statusUI.Init();
            _controlUI = transform.Find("ControlUI").GetComponentInChildren<ControlUI>();
            _controlUI.Init();

            // 로비 UI 표시 및 로비를 제외한 나머지 UI는 비활성화

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
            if(forceHide)
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
    }
}