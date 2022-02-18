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
                _textReadPanel.SetActive(false);
            }

            // 알림 UI 초기 비활성화
            ShowTextMessage("", true);
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

        // UI에 지정 텍스트 표시
        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            // 강제 숨김
            if(forceHide)
            {
                _textReadPanel.SetActive(false);
            }
            else
            {
                // show = 텍스트 창 표시 상태
                bool isShow = _textReadPanel.activeSelf;

                if (_textReadPanel != null)
                {
                    // UI 텍스트 창 활성화(꺼져있는 경우), 비활성화(켜져있는 경우)
                    if (isShow == false)
                        _textReadPanel.SetActive(true);
                    else
                        _textReadPanel.SetActive(false);

                    // 활성화 되었으면 텍스트 타이핑 동작
                    if (isShow == false)
                    {
                        // 텍스트 넣기
                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init();
                    }
                }
            }
        }
    }
}