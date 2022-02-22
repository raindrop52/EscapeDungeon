using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EscapeGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _inst;

        public Joystick _joystick;
        GameObject _textReadPanel;

        #region UI
        public StatusUI _statusUI;
        #endregion

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            _textReadPanel = GetComponentInChildren<WriteTyping>().gameObject;
            if(_textReadPanel.activeSelf)
            {
                // 초기화 시 활성화 상태인 경우 비활성화 처리
                ShowTextPanel(false);
            }

            // UI 초기화
            // 로비 UI 초기화

            // 플레이 UI 초기화
            _statusUI = transform.Find("StatusUI").GetComponentInChildren<StatusUI>();
            _statusUI.Init();

            // 로비 UI 표시 및 로비를 제외한 나머지 UI는 비활성화

        }

        public void OnPointerDown()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnPointerDown(data);
        }

        public void OnPointerUp()
        {
            _joystick.OnPointerUp(null);
        }

        public void OnDrag()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnDrag(data);
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
                ShowTextPanel(false);
            }
            else
            {
                // show = 텍스트 창 표시 상태
                bool isShow = _textReadPanel.activeSelf;

                if (_textReadPanel != null)
                {
                    // UI 텍스트 창 활성화(꺼져있는 경우), 비활성화(켜져있는 경우)
                    if (isShow == false)
                        ShowTextPanel(true);
                    else
                        ShowTextPanel(false);

                    // 활성화 되었으면 텍스트 타이핑 동작
                    if (isShow == false)
                    {
                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init(delegate ()
                        {
                            // 텍스트 메시지 창 닫기
                            ShowTextPanel(false);
                        });
                    }
                }
            }
        }
    }
}